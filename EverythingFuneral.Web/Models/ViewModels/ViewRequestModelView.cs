using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverythingFuneral.Web.Models.ViewModels {
    public class ViewRequestModelView {
        public Guid RecordId { get; set; }
        public string MembershipReference { get; set; }
        public string EmailAddres { get; set; }
        public string IDNumber { get; set; }
        public string ContactNumber { get; set; }
        public string FullName { get; set; }
        public string FuneralOption { get; set; }
        public string RequestStatus { get; set; }
    }
}