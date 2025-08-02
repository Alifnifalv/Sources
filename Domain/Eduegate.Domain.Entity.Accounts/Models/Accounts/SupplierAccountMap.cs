using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("SupplierAccountMaps", Schema = "account")]
    public partial class SupplierAccountMap
    {
        [Key]
        public long SupplierAccountMapIID { get; set; }

        public long? SupplierID { get; set; }

        public long? AccountID { get; set; }

        public byte? EntitlementID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual Account Account { get; set; }

        public virtual EntityTypeEntitlement Entitlement { get; set; }
    }
}