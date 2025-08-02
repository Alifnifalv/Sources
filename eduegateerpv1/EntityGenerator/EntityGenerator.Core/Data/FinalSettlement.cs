using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FinalSettlement", Schema = "schools")]
    public partial class FinalSettlement
    {
        public FinalSettlement()
        {
            FinalSettlementFeeTypeMaps = new HashSet<FinalSettlementFeeTypeMap>();
            FinalSettlementPaymentModeMaps = new HashSet<FinalSettlementPaymentModeMap>();
        }

        [Key]
        public long FinalSettlementIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FinalSettlementDate { get; set; }
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
        public string Remarks { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("FinalSettlements")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("FinalSettlements")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("FinalSettlements")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("FinalSettlements")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("FinalSettlements")]
        public virtual Student Student { get; set; }
        [InverseProperty("FinalSettlement")]
        public virtual ICollection<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }
        [InverseProperty("FinalSettlement")]
        public virtual ICollection<FinalSettlementPaymentModeMap> FinalSettlementPaymentModeMaps { get; set; }
    }
}
