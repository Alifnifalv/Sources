using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeCollectionCaseSearchView
    {
        public long FeeCollectionIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(500)]
        public string EmailID { get; set; }
        [StringLength(50)]
        public string School { get; set; }
        public long? StudentID { get; set; }
        public long? ClassFeeMasterIID { get; set; }
        [StringLength(4000)]
        public string FeeMasterID { get; set; }
        [StringLength(50)]
        public string feeReceiptNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CollectionDate { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? TaxAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? FineAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? PaidAmount { get; set; }
        public bool? IsPaid { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? AcadamicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [StringLength(50)]
        public string GroupTransactionNumber { get; set; }
        public int? FeeCollectionStatusID { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string FeeCollectionStatus { get; set; }
        public string ReportName { get; set; }
    }
}
