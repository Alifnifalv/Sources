using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Refund", Schema = "schools")]
    public partial class Refund
    {
        public Refund()
        {
            RefundFeeTypeMaps = new HashSet<RefundFeeTypeMap>();
            RefundPaymentModeMaps = new HashSet<RefundPaymentModeMap>();
        }

        [Key]
        public long RefundIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RefundDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaidAmount { get; set; }
        public bool? IsPaid { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        public bool IsAccountPosted { get; set; }
        public int? AcadamicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public bool? IsCancelled { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelledDate { get; set; }
        [StringLength(250)]
        public string CancelReason { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("Refunds")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("Refunds")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Refunds")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("Refunds")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("Refunds")]
        public virtual Student Student { get; set; }
        [InverseProperty("Refund")]
        public virtual ICollection<RefundFeeTypeMap> RefundFeeTypeMaps { get; set; }
        [InverseProperty("Refund")]
        public virtual ICollection<RefundPaymentModeMap> RefundPaymentModeMaps { get; set; }
    }
}
