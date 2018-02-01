using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EverythingFuneral.Web.Models.ViewModels {
    public class PostalAddress {
        [DisplayName("Is Postal Address Same As Residential?")]
        public string PostalSameAsResidential { get; set; }
        [DisplayName("Postal Address Line 1")]
        public string PostalAddressLineOne { get; set; }
        [DisplayName("Postal Address Line 2")]
        public string PostalAddressLineTwo { get; set; }
        [DisplayName("Postal Address City")]
        public string PostalAddressCity { get; set; }
        [DisplayName("Postal Address Code")]
        public string PostalAddressCode { get; set; }
    }
}