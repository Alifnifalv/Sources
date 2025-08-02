using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeCategoryMapsView
    {
        public long CategoryFeeMapIID { get; set; }
        public long? FeeMasterID { get; set; }
        [StringLength(50)]
        public string FeeCode { get; set; }
        [StringLength(50)]
        public string FeeMaster { get; set; }
        public bool? IsPrimary { get; set; }
        public long? CategoryID { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string IsPrimaryValue { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
