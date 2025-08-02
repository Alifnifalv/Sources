using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class RefundSearch
    {
        public long RefundIID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        public long? StudentID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
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
        [StringLength(10)]
        [Unicode(false)]
        public string RefundDate { get; set; }
        public bool? IsPaid { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? RefundAmount { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Balance { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        public bool IsAccountPosted { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(126)]
        public string AcadamicYear { get; set; }
    }
}
