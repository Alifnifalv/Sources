using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeCollections", Schema = "schools")]
    [Index("CollectionDate", Name = "IDX_FeeCollections_CollectionDate")]
    [Index("GroupTransactionNumber", Name = "IDX_FeeCollections_GroupTransactionNumber_ClassID__SectionID__StudentID__FeeMasterID__CollectionDat")]
    [Index("StudentID", Name = "IDX_FeeCollections_StudentID")]
    [Index("FeeCollectionIID", "ClassID", "AcadamicYearID", "StudentID", Name = "IX_FeeCollections")]
    public partial class FeeCollection
    {
        public FeeCollection()
        {
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeCollectionPaymentModeMaps = new HashSet<FeeCollectionPaymentModeMap>();
        }

        [Key]
        public long FeeCollectionIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? StudentID { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CollectionDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? FineAmount { get; set; }
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
        public long? ClassFeeMasterId { get; set; }
        public bool IsAccountPosted { get; set; }
        public int? AcadamicYearID { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        public byte? SchoolID { get; set; }
        public long? CashierID { get; set; }
        public string Remarks { get; set; }
        public bool? IsCancelled { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelledDate { get; set; }
        [StringLength(250)]
        public string CancelReason { get; set; }
        public int? FeeCollectionStatusID { get; set; }
        [StringLength(50)]
        public string GroupTransactionNumber { get; set; }
        public long? PaymentTrackID { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("FeeCollections")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("FeeCollections")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("CashierID")]
        [InverseProperty("FeeCollections")]
        public virtual Employee Cashier { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("FeeCollections")]
        public virtual Class Class { get; set; }
        [ForeignKey("ClassFeeMasterId")]
        [InverseProperty("FeeCollections")]
        public virtual ClassFeeMaster ClassFeeMaster { get; set; }
        [ForeignKey("FeeCollectionStatusID")]
        [InverseProperty("FeeCollections")]
        public virtual FeeCollectionStatus FeeCollectionStatus { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("FeeCollections")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("FeeCollections")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("FeeCollections")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("FeeCollections")]
        public virtual Student Student { get; set; }
        [InverseProperty("FeeCollection")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        [InverseProperty("FeeCollection")]
        public virtual ICollection<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }
    }
}
