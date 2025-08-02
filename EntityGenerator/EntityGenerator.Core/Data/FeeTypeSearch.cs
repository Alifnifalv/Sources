using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeTypeSearch
    {
        public int FeeTypeID { get; set; }
        [Required]
        [StringLength(20)]
        public string FeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }
        public int? FeeGroupId { get; set; }
        public byte? FeeCycleId { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Required]
        [StringLength(14)]
        [Unicode(false)]
        public string IsRefundable { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
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
        public byte[] TimeStamps { get; set; }
        [StringLength(100)]
        public string FeeGroup { get; set; }
        [StringLength(50)]
        public string FeeCycle { get; set; }
    }
}
