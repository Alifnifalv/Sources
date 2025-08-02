using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetTransactionHead", Schema = "asset")]
    public partial class AssetTransactionHead
    {
        public AssetTransactionHead()
        {
            AssetInventoryTransactions = new HashSet<AssetInventoryTransaction>();
            AssetTransactionDetails = new HashSet<AssetTransactionDetail>();
            AssetTransactionHeadAccountMaps = new HashSet<AssetTransactionHeadAccountMap>();
        }

        [Key]
        public long HeadIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        public long? DocumentStatusID { get; set; }
        public byte? ProcessingStatusID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CompanyID { get; set; }
        public long? BranchID { get; set; }
        public long? ToBranchID { get; set; }
        [StringLength(50)]
        public string TransactionNo { get; set; }
        public long? ReferenceHeadID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        public string Reference { get; set; }
        public long? DepartmentID { get; set; }
        [StringLength(100)]
        public string AssetLocation { get; set; }
        [StringLength(100)]
        public string SubLocation { get; set; }
        [StringLength(10)]
        public string AssetFloor { get; set; }
        [StringLength(10)]
        public string RoomNumber { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        public long? SupplierID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DepreciationStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DepreciationEndDate { get; set; }

        [ForeignKey("DocumentStatusID")]
        [InverseProperty("AssetTransactionHeads")]
        public virtual DocumentReferenceStatusMap DocumentStatus { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("AssetTransactionHeads")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("ProcessingStatusID")]
        [InverseProperty("AssetTransactionHeads")]
        public virtual TransactionStatus ProcessingStatus { get; set; }
        [InverseProperty("AssetTransactionHead")]
        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        [InverseProperty("AssetTransactionHead")]
        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
    }
}
