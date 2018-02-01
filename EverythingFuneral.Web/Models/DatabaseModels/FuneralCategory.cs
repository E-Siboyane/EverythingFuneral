using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EverythingFuneral.Web.Models.DatabaseModels {
    public class FuneralCategory {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FuneralCategoryId { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public decimal PayoutAmount { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? LastModified { get; set; }
        [Required]
        public string RecordCode { get; set; }
        public virtual RecordStatus RecordStatus { get; set; }
    }
}