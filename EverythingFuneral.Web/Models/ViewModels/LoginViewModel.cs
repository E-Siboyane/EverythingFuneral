using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EverythingFuneral.Web.Models.ViewModels {
    public class LoginViewModel {
        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Please enter valid email address")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}