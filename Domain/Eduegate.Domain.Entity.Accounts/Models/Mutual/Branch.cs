using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Domain.Entity.Accounts.Models.Jobs;
using Eduegate.Domain.Entity.Accounts.Models.Payrolls;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("Branches", Schema = "mutual")]
    public partial class Branch
    {
        public Branch()
        {
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            AssetInventories = new HashSet<AssetInventory>();
            AssetInventoryTransactions = new HashSet<AssetInventoryTransaction>();
            EmployeePromotionNewBranches = new HashSet<EmployeePromotion>();
            EmployeePromotionOldBranches = new HashSet<EmployeePromotion>();
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
        public long BranchIID { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }

        public long? BranchGroupID { get; set; }

        public long? WarehouseID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public bool? IsMarketPlace { get; set; }

        public byte? StatusID { get; set; }

        [StringLength(500)]
        public string Logo { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string Longitude { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string Latitude { get; set; }

        public bool? IsVirtual { get; set; }

        [StringLength(50)]
        public string BranchCode { get; set; }

        [StringLength(50)]
        public string TransactionNoPrefix { get; set; }

        public int? SortOrder { get; set; }

        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        public virtual ICollection<AssetInventory> AssetInventories { get; set; }

        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }

        public virtual ICollection<EmployeePromotion> EmployeePromotionNewBranches { get; set; }

        public virtual ICollection<EmployeePromotion> EmployeePromotionOldBranches { get; set; }

        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}