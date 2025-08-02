using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("CustomerAccountMaps", Schema = "account")]
    public partial class CustomerAccountMap
    {
        [Key]
        public long CustomerAccountMapIID { get; set; }

        public long CustomerID { get; set; }

        public long? AccountID { get; set; }

        public byte? EntitlementID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual Account Account { get; set; }

        //public virtual Customer Customer { get; set; }

        public virtual EntityTypeEntitlement Entitlement { get; set; }
    }
}