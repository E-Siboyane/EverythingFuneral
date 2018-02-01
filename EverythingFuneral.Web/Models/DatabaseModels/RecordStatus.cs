using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EverythingFuneral.Web.Models.DatabaseModels {
    public class RecordStatus {
        [Key]
        public string RecordCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}