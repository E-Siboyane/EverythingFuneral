using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EverythingFuneral.Web.Models.ViewModels {
    public class ResidentialAddress {
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
    }
}