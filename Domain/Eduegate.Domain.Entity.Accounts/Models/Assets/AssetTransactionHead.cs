using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Eduegate.Domain.Entity.Accounts.Models.Inventory;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
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

        public DateTime? EntryDate { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public long? DocumentStatusID { get; set; }

        public byte? ProcessingStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CompanyID { get; set; }

        public long? BranchID { get; set; }

        public long? ToBranchID { get; set; }

        [StringLength(50)]
        public string TransactionNo { get; set; }

        public long? ReferenceHeadID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? SupplierID { get; set; }

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

        public DateTime? DepreciationStartDate { get; set; }

        public DateTime? DepreciationEndDate { get; set; }

        public virtual DocumentReferenceStatusMap DocumentStatus { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual TransactionStatus ProcessingStatus { get; set; }

        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }

        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }

        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
    }
}