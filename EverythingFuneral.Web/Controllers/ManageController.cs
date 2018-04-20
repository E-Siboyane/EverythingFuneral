using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using EverythingFuneral.Web.App_Start;
using EverythingFuneral.Web.Models.DatabaseModels;
using EverythingFuneral.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;

namespace EverythingFuneral.Web.Controllers {
    [HandleError]
    public class ManageController : Controller {
        private ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController() {
            _context = new ApplicationDbContext();
        }

        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public ActionResult RequestService(int? planId) {
            try {
                var funeralRequest = new RequestService();

                if (!planId.HasValue)
                    return RedirectToAction("index", "Home");

                funeralRequest.FuneralCategoryId = (int)planId;
                funeralRequest.RecordUniqueId = Guid.NewGuid();
                funeralRequest.RecordCode = "ACT";
                funeralRequest.CreatedDate = DateTime.Now;
                funeralRequest.FuneralOptions = GetFuneralOptions();
                funeralRequest.SelectYesNoOptions = GetYesNoOptions();
                funeralRequest.SelectProvinces = GetProvinces();
                funeralRequest.SelectCountries = GetCountries();
                funeralRequest.SelectHighestQualifaction = GetHighestQualificaationOptions();
                funeralRequest.SelectLanguages = GetHomeLanguages();
                funeralRequest.SelectGraveyardTypes = GetGraveyardType();
                funeralRequest.SelectMortuaryTypes = GetMortuaryTypes();
                funeralRequest.SelectTombstoneTypes = GetTombstoneTypes();
                funeralRequest.SelectFlowerTypes = GetFlowerTypes();
                funeralRequest.SelectMaritalStatus = GetMaritalStatus();
                return View(funeralRequest);
            }
            catch (Exception exception) {
                throw exception;
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult RequestServiceAuth(Guid recordNumber) {
            try {
                var funeralRequest = new RequestService();

                if (recordNumber == null)
                    return RedirectToAction("index", "Home");

                var userFuneralOptions = GetMemberDetails(recordNumber);
                funeralRequest = userFuneralOptions;
                funeralRequest.ConfrimEmail = funeralRequest.Username;
                funeralRequest.Password = "EFTempPass9870KL$#!00";
                funeralRequest.ConfirmPassword = "EFTempPass9870KL$#!00";

                //Load Select Options
                funeralRequest.FuneralOptions = GetFuneralOptions();
                funeralRequest.SelectYesNoOptions = GetYesNoOptions();
                funeralRequest.SelectProvinces = GetProvinces();
                funeralRequest.SelectCountries = GetCountries();
                funeralRequest.SelectHighestQualifaction = GetHighestQualificaationOptions();
                funeralRequest.SelectLanguages = GetHomeLanguages();
                funeralRequest.SelectGraveyardTypes = GetGraveyardType();
                funeralRequest.SelectMortuaryTypes = GetMortuaryTypes();
                funeralRequest.SelectTombstoneTypes = GetTombstoneTypes();
                funeralRequest.SelectFlowerTypes = GetFlowerTypes();
                funeralRequest.SelectMaritalStatus = GetMaritalStatus();
                return View(funeralRequest);
            }
            catch (Exception exception) {
                throw exception;
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult RequestServiceAuth(RequestService funeralRequest) {
            try {
                if (ModelState.IsValid) {
                    if (UpdateRequest(funeralRequest)) {
                        return RedirectToAction("Success", new { recordNumber = funeralRequest.RecordUniqueId });
                    }
                    else {
                        ViewBag.Errors = true;
                        ViewBag.ValidationErrorMessage = "An error has occurred while trying to save the information, please try again.";
                        return View(funeralRequest);
                    }
                }

                ViewBag.Errors = true;
                var validationError = ModelState.Values.SelectMany(v => v.Errors);
                string message = string.Empty;
                foreach (var item in validationError) {
                    message += item.ErrorMessage.ToString() + "<br>";
                }
                ViewBag.ValidationErrorMessage = message;
                return View(funeralRequest);
            }
            catch (Exception exception) {
                throw exception;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ViewRequest() {
            var activeRequests = new List<ViewRequestModelView>();
             try {
                ViewBag.Message = string.Empty;
                var results = _context.ClientDetails.Where(x => string.Compare(x.RecordCode, Models.DTO.RecordStatus.ACT.ToString(), true) == 0).
                              Include(x => x.FuneralOption).Include(x => x.RecordStatus).ToList();
                foreach(var item in results) {
                    activeRequests.Add(new ViewRequestModelView() {
                        RecordId = item.RecordUniqueId,
                        MembershipReference = item.MemebershipReference,
                        EmailAddres = item.Username,
                        IDNumber = item.IdNumber,
                        ContactNumber = item.CellNumber,
                        FullName = string.Format("{0} {1}", item.Surname, item.FullName),
                        FuneralOption = item.FuneralOption.DisplayName,
                        RequestStatus = item.RecordStatus.Name
                    });
                }

                if (TempData["RecordCompleted"] != null) {
                    ViewBag.Message = TempData["RecordCompleted"];
                }
                
                return View(activeRequests);
            }
            catch (Exception exception) {
                throw exception;
            }
        }

        [HttpGet]
        public ActionResult CompleteRequest(Guid recordNumber) {
            try {
                var Id = _context.ClientDetails.FirstOrDefault(x => x.RecordUniqueId == recordNumber).ClientDetailId;
                var UpdateRecord = _context.ClientDetails.Find(Id);
                UpdateRecord.RecordCode = "NA";
                UpdateRecord.LastModified = DateTime.Now;
                _context.SaveChanges();
                TempData["RecordCompleted"] = string.Format("Successfully updated Membership Reference Number : {0}, with Option {1} to Completed",
                                                            UpdateRecord.MemebershipReference, UpdateRecord.FuneralOption.DisplayName);
                return RedirectToAction("ViewRequest");
            }
            catch (Exception exception) {
                throw exception;
            }
        }

        public RequestService GetMemberDetails(Guid recordUniqueId) {
            var funeralRequest = new RequestService();
            var funeralOptions = _context.ClientDetails.FirstOrDefault(x => x.RecordUniqueId == recordUniqueId);

            if (funeralOptions != null) {
                funeralRequest = JsonConvert.DeserializeObject<RequestService>(JsonConvert.SerializeObject(funeralOptions));
            }
            return (funeralRequest);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestService(RequestService funeralRequest) {
            try {
                if (ModelState.IsValid) {

                    var registerUser = await RegisterUser(funeralRequest.ConfrimEmail, funeralRequest.ConfirmPassword);
                    if (string.Compare(registerUser.ToLower(), true.ToString().ToLower(), true) == 0) {
                        var serialiseRequest = JsonConvert.DeserializeObject<ClientDetail>(JsonConvert.SerializeObject(funeralRequest));
                        var id = CreateRequest(serialiseRequest);
                        if (id == Guid.Empty) {
                            ViewBag.ValidationErrorMessage = "An error has occurred while trying to save the information, please try again.";
                            return View(funeralRequest);
                        }
                        else {
                            var result = await SignInManager.PasswordSignInAsync(funeralRequest.Username, funeralRequest.Password, false, shouldLockout: false);
                            return RedirectToAction("Success", new { recordNumber = id });                           
                        }
                    }
                    else {
                        ViewBag.ValidationErrorMessage = registerUser;
                    }
                }
                else {
                    ViewBag.Errors = true;
                    var validationError = ModelState.Values.SelectMany(v => v.Errors);
                    string message = string.Empty;
                    foreach (var item in validationError) {
                        message += item.ErrorMessage.ToString() + "<br>";
                    }
                    ViewBag.ValidationErrorMessage = message;
                }

                return View(funeralRequest);
            }
            catch (Exception exception) {
                throw exception;
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Success(Guid? recordNumber) {
            if (recordNumber != null) {
                var record = GetMemberRecord((Guid)recordNumber);
                return View(record);
            }            
            return RedirectToAction("Index", "Home");
        }

        public Guid CreateRequest(ClientDetail clientDetails) {
            _context.ClientDetails.Add(clientDetails);
            _context.SaveChanges();
            UpdateMembershipReference(clientDetails.ClientDetailId);
            var sendRequest = SendNewRequestEmail(clientDetails.RecordUniqueId);
            return clientDetails.RecordUniqueId;
        }

        public bool UpdateRequest(RequestService requestService) {
     
            var status = false;
            var service = _context.ClientDetails.Find(requestService.ClientDetailId);
            if(service != null) {
                var serialisedRequest = JsonConvert.DeserializeObject<ClientDetail>(JsonConvert.SerializeObject(requestService));
                serialisedRequest.LastModified = DateTime.Now;
                _context.Entry(service).CurrentValues.SetValues(serialisedRequest);
                _context.SaveChanges();
                status = true;
            }            
            return (status);
        }

        private string  GetSelectedFuneralOption(int id) {
            var option = string.Empty;
            var category = _context.FuneralCategories.Find(id);
            if (category != null)
                option = category.DisplayName;
            return (option);
        }

        public bool SendNewRequestEmail(Guid recordNumber) {
            var baseUrl = ConfigurationManager.AppSettings.Get("BaseUrl");
            var fromEmail = ConfigurationManager.AppSettings.Get("FromEmail");
            var toEmail = ConfigurationManager.AppSettings.Get("ToEmail");
            var accountPassord = ConfigurationManager.AppSettings.Get("AccountPassword");
            var accountUsername = ConfigurationManager.AppSettings.Get("AccountUsername");
            var smtp = ConfigurationManager.AppSettings.Get("SMTP");
            var templatePath = HostingEnvironment.MapPath("~/Template/NewRequest.html");
            var clientDetails = GetMemberRecord(recordNumber);
            var funeralOption = GetSelectedFuneralOption(clientDetails.FuneralCategoryId);
            var authRequestUrl = string.Format("{0}/RequestServiceAuth?recordNumber={1}", "Manage", clientDetails.RecordUniqueId);
            var url = string.Format("{0}{1}", baseUrl, authRequestUrl);
            var subject = string.Format("NEW FUNERAL ARRANGEMENT REQUEST - {0}", clientDetails.MemebershipReference);
            var template = string.Empty;
            template = FormatEmailTemplate(templatePath, clientDetails, funeralOption, url);

            try {
                string mailBodyhtml = template;
                var msg = new MailMessage(fromEmail, toEmail, subject, mailBodyhtml);
                msg.IsBodyHtml = true;
                var smtpClient = new SmtpClient(smtp, 587);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(accountUsername, accountPassord);
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            }
            catch (Exception e) {

            }
            return true;
        }

        private static string FormatEmailTemplate(string templatePath, RequestService clientDetails, string funeralOption, string url) {
            var  template = string.Empty;
            using (StreamReader reader = new StreamReader(@templatePath)) {
                string contents = reader.ReadToEnd();
                template = contents.Replace("{ReferenceNumber}", clientDetails.MemebershipReference).
                          Replace("{Name}", string.Format("{0} {1}", clientDetails.Surname, clientDetails.FullName)).
                          Replace("{FuneralOption}", funeralOption).
                          Replace("{cellphoneNumber}", clientDetails.CellNumber).
                          Replace("{IdPassportNumber}", clientDetails.IdNumber).
                          Replace("{Email}", clientDetails.Username).
                          Replace("{url}", url);
            }

            return template;
        }

        public RequestService GetMemberRecord(Guid recordNumber) {
            var service = _context.ClientDetails.Where(x => x.RecordUniqueId == recordNumber).
                         OrderByDescending(x => x.ClientDetailId).FirstOrDefault();
            if (service != null) {
                var serialisedRequest = JsonConvert.DeserializeObject<RequestService>(JsonConvert.SerializeObject(service));
                return (serialisedRequest);
            }
            return (new RequestService());
        }

        private bool UpdateMembershipReference(int clientId) {
            var client = _context.ClientDetails.Find(clientId);
            client.MemebershipReference = string.Format("EF-{0}", client.ClientDetailId.ToString().PadLeft(7, '0'));
            _context.SaveChanges();
            return true;
        }

        public async Task<string> RegisterUser(string userEmail, string password) {
            var result = string.Empty;
            if (ModelState.IsValid) {
                var user = new ApplicationUser() {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };
                var createUser = await UserManager.CreateAsync(user, password);
                if (createUser.Succeeded) {
                    return (true.ToString());
                }
                else {

                    var row = 1;
                    foreach (var error in createUser.Errors) {
                        if (createUser.Errors.Count() > 1) {
                            if (row == 1)
                                result = result + error;
                            else
                                result = result + "<br>" + error;
                        }
                        else {
                            result = result + error;
                        }
                        row++;
                    }
                }
            }
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            var loginModel = new LoginViewModel();
            return View(loginModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginModel, string returnUrl) {
            if (!ModelState.IsValid) {
                return View(loginModel);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, loginModel.RememberMe, shouldLockout: false);
            switch (result) {
                case SignInStatus.Success:
                    ViewBag.FullName = "Elias Seboyane";
                    return RedirectToLocal(returnUrl, loginModel.Username);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(loginModel);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl, string username) {
            var user = _context.Users.FirstOrDefault(x => string.Compare(x.UserName, username, true) == 0);
            if (SignInManager.UserManager.IsInRole(user.Id, "Admin")) {
                return RedirectToAction("ViewRequest");
            }
            else {
                var userRecord = _context.ClientDetails.FirstOrDefault(x => string.Compare(x.Username, username, true) == 0);
                return RedirectToAction("RequestServiceAuth", new { recordNumber = userRecord.RecordUniqueId });
            }
        }

        [HttpGet]
        public ActionResult LogOff() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        public JsonResult IsEmailUsed(string email) {
            var status = false;
            try {
                var result = _context.Users.Where(u => string.Compare(u.UserName, email, true) == 0).FirstOrDefault();
                if (result == null) {
                    status = true;
                }
            }
            catch {
                status = false;
            }
            return (Json(status));
        }
        public List<SelectOptions> GetFuneralOptions() {
            var funeralOptions = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Select Funeral Option -", DisplayValue = "" }
            };
            var retrieveDbFuneralOPtions = _context.FuneralCategories.
                                Where(x => x.DeletedDate == null && string.Compare(x.RecordCode, "ACT", true) == 0).ToList();
            foreach (var item in retrieveDbFuneralOPtions) {
                funeralOptions.Add(new SelectOptions { DisplayText = item.DisplayName, DisplayValue = item.FuneralCategoryId.ToString() });
            }
            return (funeralOptions);
        }

        public List<SelectOptions> GetYesNoOptions() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Option -", DisplayValue = "" },
                new SelectOptions { DisplayValue = "Yes", DisplayText = "Yes" },
                new SelectOptions { DisplayText = "No", DisplayValue = "No" }
            };
            return (options);
        }

        public List<SelectOptions> GetHighestQualificaationOptions() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Highest Qualification -", DisplayValue = "" },
                new SelectOptions { DisplayText = "Matric Certificate", DisplayValue  = "Matric Certificate" },
                new SelectOptions { DisplayText = "Diploma", DisplayValue = "Diploma" },
                new SelectOptions { DisplayText = "Under Graduate Degree", DisplayValue = "Under Graduate Degree" },
                new SelectOptions { DisplayText = "Post Graduate Degree", DisplayValue = "Post Graduate Degree" },
                new SelectOptions { DisplayText = "Other", DisplayValue = "Other" }
            };
            return (options);
        }

        public List<SelectOptions> GetProvinces() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Province -", DisplayValue = "" },
                new SelectOptions { DisplayText = "Gauteng", DisplayValue  = "Gauteng" },
                new SelectOptions { DisplayText = "Mpumalanga", DisplayValue  = "Mpumalanga" },
                new SelectOptions { DisplayText = "Limpopo", DisplayValue  = "Limpopo" },
                new SelectOptions { DisplayText = "North West", DisplayValue  = "North West" },
                new SelectOptions { DisplayText = "Free State", DisplayValue  = "Free State" },
                new SelectOptions { DisplayText = "Eastern Cape", DisplayValue = "Eastern Cape" },
                new SelectOptions { DisplayText = "Western Cape", DisplayValue = "Western Cape" },
                new SelectOptions { DisplayText = "Northern Cape", DisplayValue = "Northern Cape" },
                new SelectOptions { DisplayText = "Kwa-Zulu Natal", DisplayValue = "Kwa-Zulu Natal" }
            };
            return (options);
        }

        public List<SelectOptions> GetCountries() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Country Of Issue -", DisplayValue = "" },
                new SelectOptions { DisplayText = "South Africa", DisplayValue = "South Africa"  },
                new SelectOptions { DisplayValue = "Other", DisplayText = "Other" }
            };
            return (options);
        }

        public List<SelectOptions> GetHomeLanguages() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Home Language -", DisplayValue = "" },
                new SelectOptions { DisplayText = "English", DisplayValue = "English" },
                new SelectOptions { DisplayText = "Afrikaans", DisplayValue = "Afrikaans" },
                new SelectOptions { DisplayText = "Northern Sotho", DisplayValue = "Northern Sotho" },
                new SelectOptions { DisplayText = "Southern Sotho", DisplayValue = "Southern Sotho" },
                new SelectOptions {  DisplayText = "Tswana", DisplayValue = "Tswana" },
                new SelectOptions { DisplayText = "Venda", DisplayValue = "Venda" },
                new SelectOptions { DisplayText = "Tsonga", DisplayValue = "Tsonga" },
                new SelectOptions { DisplayText = "Zulu", DisplayValue = "Zulu" },
                new SelectOptions { DisplayText = "Xhosa", DisplayValue = "Xhosa" },
                new SelectOptions { DisplayText = "Swati", DisplayValue = "Swati" },
                new SelectOptions { DisplayText = "Ndebele", DisplayValue = "Ndebele" }
            };
            return (options);
        }

        public List<SelectOptions> GetGraveyardType() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Graveyard Type -", DisplayValue = "" },
                new SelectOptions { DisplayText = "Public", DisplayValue = "Public" },
                 new SelectOptions { DisplayText = "Private", DisplayValue = "Private" },
                new SelectOptions { DisplayValue = "Other", DisplayText = "Other" }
            };
            return (options);
        }

        public List<SelectOptions> GetMortuaryTypes() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Mortuary Type -", DisplayValue = "" },
                new SelectOptions { DisplayText = "Public", DisplayValue = "Public" },
                 new SelectOptions { DisplayText = "Private", DisplayValue = "Private" },
                new SelectOptions { DisplayValue = "Other", DisplayText = "Other" }
            };
            return (options);
        }

        public List<SelectOptions> GetTombstoneTypes() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Mortuary Type -", DisplayValue = "" },
                new SelectOptions { DisplayText = "Not Required", DisplayValue = "Not Required" },
                new SelectOptions { DisplayText = "Standard", DisplayValue = "Standard" },
                new SelectOptions { DisplayText = "Executive", DisplayValue = "Executive" },
                new SelectOptions { DisplayText = "Executive", DisplayValue = "Royal" },
                new SelectOptions { DisplayValue = "Other", DisplayText = "Other" }
            };
            return (options);
        }
        public List<SelectOptions> GetFlowerTypes() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Flowers -", DisplayValue = "" },
                new SelectOptions { DisplayText = "Not Required", DisplayValue = "Not Required" },
                new SelectOptions { DisplayText = "Fresh", DisplayValue = "Fresh" },
                new SelectOptions { DisplayText = "Synthetic", DisplayValue = "Synthetic" }
            };
            return (options);
        }

        public List<SelectOptions> GetMaritalStatus() {
            var options = new List<SelectOptions>() {
                new SelectOptions { DisplayText = "- Please Select Marital Status -", DisplayValue = "" },
                new SelectOptions { DisplayText = "Single", DisplayValue = "Single" },
                new SelectOptions { DisplayText = "Married", DisplayValue = "Married" },
                new SelectOptions { DisplayText = "Divorced", DisplayValue = "Divorced" },
                new SelectOptions { DisplayText = "Widowed", DisplayValue = "Widowed" }
            };
            return (options);
        }

        // GET: Home
        public ActionResult ViewEmbedPDF() {
            return View();
        }

        [HttpPost]
        public ActionResult ViewPDF() {
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/Template/20180208.pdf"));

            return RedirectToAction("ViewEmbedPDF");
        }
    }
}