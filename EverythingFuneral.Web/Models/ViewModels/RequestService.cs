using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EverythingFuneral.Web.Models.ViewModels {
    public class RequestService {
        public int ClientDetailId { get; set; }
        public Guid RecordUniqueId { get; set; }
        public string MemebershipReference { get; set; }
        [Required, DisplayName("Email Address"), DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required, DisplayName("Confirm Email Address"), DataType(DataType.EmailAddress)]
        [Compare("Username", ErrorMessage = "The Email and Confirmation Email do not match.")]
        public string ConfrimEmail { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required, DisplayName("Funeral Option")]
        public int FuneralCategoryId { get; set; }
        [Required, DisplayName("Full Names")]
        public string FullName { get; set; }
        [Required, DisplayName("Surname")]
        public string Surname { get; set; }
        [Required, DisplayName("ID Number/Passport Number")]
        public string IdNumber { get; set; }
        [Required, DisplayName("Country Of Issue")]
        public string CountryOfIssue { get; set; }
        [Required, DisplayName("Highest Qualification")]
        public string HighestQualification { get; set; }
        [Required, DisplayName("Home Language")]
        public string HomeLanguage { get; set; }
        [Required, DisplayName("Residential Address Street Address")]
        public string ResidentialAddressStreetAddress { get; set; }
        [Required, DisplayName("Residential Address Surbub")]
        public string ResidentialAddressSurbub { get; set; }
        [Required, DisplayName("Residential Address City")]
        public string ResidentialAddressCity { get; set; }
        [Required, DisplayName(" Residential Address Province")]
        public string ResidentialAddressProvince { get; set; }
        [Required, DisplayName("Residential Address Country")]
        public string ResidentialAddressCountry { get; set; }
        [Required, DisplayName("Postal Code")]
        public string ResidentialAddressPostalCode { get; set; }
        [DisplayName("Is Postal Address Same As Residential?")]
        public string PostalAddressSameResidential { get; set; }
        [DisplayName("Postal Address Line 1")]
        public string PostalAddressLineOne { get; set; }
        [DisplayName("Postal Address Line 2")]
        public string PostalAddressLineTwo { get; set; }
        [DisplayName("Postal Address City")]
        public string PostalAddressCity { get; set; }
        [DisplayName("Postal Address Code")]
        public string PostalAddressCode { get; set; }
        [Required, DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }
        [Required, DisplayName("Number Of Dependents")]
        public string NumberOfDependent { get; set; }
        [Required, DisplayName("Employer Name")]
        public string EmployerName { get; set; }
        [Required, DisplayName("Industry")]
        public string Industry { get; set; }
        [Required, DisplayName("Number Of Years Working At Employer")]
        public string YearsWorkingAtEmployer { get; set; }
        [Required, DisplayName("Gross Salary")]
        public string GrossSalary { get; set; }
        [Required, DisplayName("Net Income")]
        public string NetIncome { get; set; }
        [Required, DisplayName("Cellphone Number")]
        public string CellNumber { get; set; }
        [DisplayName("Next Of Kin Cellphone Number")]
        public string NextOfKinCellNumber { get; set; }
        [Required, DisplayName("Next Of Kin Name")]
        public string NextOfKinName { get; set; }
        [Required, DisplayName("Can our marketing team contact your?")]
        public string AllowMarketing { get; set; }
        [DisplayName("Graveyard - Type")]
        public string GraveyardType { get; set; }
        [DisplayName("Graveyard - Other Specify")]
        public string GraveyardTypeOtherSpecify { get; set; }
        [DisplayName("Graveyard - Municipality")]
        public string Municipality { get; set; }
        [DisplayName("Graveyard - Digging")]
        public string GraveyardDigging { get; set; }
        [DisplayName("Graveyard - Closing and Clearing")]
        public string GraveyardClosingAndClearing { get; set; }
        [DisplayName("Graveyard - Other")]
        public string GraveyardOtherDetails { get; set; }
        [DisplayName("Mortuary - Type")]
        public string MortuaryName { get; set; }
        [DisplayName("Mortuary - Other, Specify")]
        public string MortuaryOther { get; set; }
        [DisplayName("Mortuary - Location")]
        public string MortuaryLocation { get; set; }
        [DisplayName("Coufin - Type")]
        public string CoufinType { get; set; }
        [DisplayName("Transportation - Hearse")]
        public string TransportationHearse { get; set; }
        [DisplayName("Transportation - Family transport")]
        public string TransportationFamily { get; set; }
        [DisplayName("Transportation - Mini bus")]
        public string TransportationMinibus { get; set; }
        [DisplayName("Transportation - Bus")]
        public string TransportationBus { get; set; }
        [DisplayName("Accommodation - Hotel Number Of Guests")]
        public string AccomodationHotelQuestNumber { get; set; }
        [DisplayName("Accommodation - B&B Number Of Guests")]
        public string AccomodationBandBQuestNumber { get; set; }
        [DisplayName("Tent/Hall - Hall Name")]
        public string HallName { get; set; }
        [DisplayName("Tent/Hall - Tent Type")]
        public string TentType { get; set; }
        [DisplayName("Tent/Hall - Toilets")]
        public string Toilet { get; set; }
        [DisplayName("Tent/Hall - Type Of Decoration")]
        public string TypeofDecoration { get; set; }
        [DisplayName("Tent/Hall - Affiliation Theme")]
        public string AffiliationTheme { get; set; }
        [DisplayName("Chairs - Types Of Chairs")]
        public string TyepOfChair { get; set; }
        [DisplayName("Chairs - Number Of Chairs")]
        public string NumberOfChair { get; set; }
        [DisplayName("Catering - How Many Inkomo/Cow?")]
        public string CateringNumberOfCow { get; set; }
        [DisplayName("Catering - How Many Sheep?")]
        public string CateringNumberOfSheep { get; set; }
        [DisplayName("Catering - How Many Goat?")]
        public string CateringNumberOfGoat { get; set; }
        [DisplayName("Catering - Number Of People")]
        public string CateringNumberOfPeople { get; set; }
        [DisplayName("Catering - Days leading to funeral(Breakfast, lunch and supper)")]
        public string CateringDayLeadingToFuneral { get; set; }
        [DisplayName("Catering - Additional Water")]
        public string CateringAdditionalWater { get; set; }
        [DisplayName("Catering - Other?")]
        public string CateringOther { get; set; }
        [DisplayName("Tombstone - Select Tombstone Type")]
        public string Tombstone { get; set; }
        [DisplayName("Tombstone - Other, Specify")]
        public string TombstoneOtherSpecify { get; set; }
        [DisplayName("Flowers - Select Flower Type")]
        public string FlowerType { get; set; }
        [DisplayName("Flowers - Please Specify")]
        public string FlowerSpecify { get; set; }
        [DisplayName("Artist - Music (DJ or Live Performance)")]
        public string ArtistMusic { get; set; }
        [DisplayName("Artist - Mic and Sound (Cord-less or Manual)")]
        public string ArtistSoundMic { get; set; }
        [DisplayName("Other Services - Minister (Service and Graveyard)")]
        public string OtherServiceMinister { get; set; }
        [DisplayName("Other Services - Service Programme Cards (Arrangement, Print Out)")]
        public string OtherServiceProgrammeCard { get; set; }
        [DisplayName("Other Services - Invitation communication (Cards, Emails, Messages, Calls etc)")]
        public string OtherServiceInvitationCommunication { get; set; }
        [DisplayName("Other Services - Family members tracing (Invitation To All Blood & Marriage Relatives (Local and Overseas))")]
        public string OtherServiceFamilyTracing { get; set; }
        [DisplayName("Other Services - Airtime/Data (To The Nominated Cellphone)")]
        public string OtherServicesCommunication { get; set; }
        [DisplayName("Other Services - Gesture/hospitality (Number Of People)")]
        public string OtherServiceGestureHospitality { get; set; }
        [DisplayName("Other Services - Special guests (Specify)")]
        public string OtherServiceSpecialQuests { get; set; }
        [DisplayName("Other Services - Mouners (Please Specify)")]
        public string OtherServiceMouners { get; set; }
        [DisplayName("Post Funeral - Sharing Of Deseased Assets (Please Specify)")]
        public string PostFuneralAssestSharing { get; set; }
        [DisplayName("Post Funeral - Other Deseased Beneficiaries Distributions (Please Specify)")]
        public string PostFuneralOtherbeneficiaryDistribution { get; set; }
        [DisplayName("Post Funeral - Counseling Services (Please Specify)")]
        public string PostFuneralCounselingService { get; set; }
        [DisplayName("Post Funeral - Recovery Process (Please Specify)")]
        public string PostFuneralRecoveryProcess { get; set; }
        [DisplayName("Post Funeral - Gifts Collection And Distributions (Please Specify)")]
        public string PostFuneralGiftManagement { get; set; }
        [DisplayName("Post Funeral - Cleaning And Clearing (Please Specify)")]
        public string PostFuneralCleansing { get; set; }
        [DisplayName("Post Funeral - Closure (Please Specify)")]
        public string PostFuneralClosure { get; set; }
        [DisplayName("Post Funeral - After-Tears (Please Specify)")]
        public string PostFuneralAfterTears { get; set; }
        [DisplayName("Special requests - Any other (Please Specify)")]
        public string SpecialRequestSpecify { get; set; }
        public string RecordCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<SelectOptions> FuneralOptions { get; set; }
        public List<SelectOptions> SelectHighestQualifaction { get; set; }
        public List<SelectOptions> SelectLanguages { get; set; }
        public List<SelectOptions> SelectYesNoOptions { get; set; }
        public List<SelectOptions> SelectCountries { get; set; }
        public List<SelectOptions> SelectProvinces { get; set; }
        public List<SelectOptions> SelectGraveyardTypes { get; set; }
        public List<SelectOptions> SelectMortuaryTypes { get; set; }
        public List<SelectOptions> SelectTombstoneTypes { get; set; }
        public List<SelectOptions> SelectFlowerTypes { get; set; }
        public List<SelectOptions> SelectMaritalStatus { get; set; }
    }
}