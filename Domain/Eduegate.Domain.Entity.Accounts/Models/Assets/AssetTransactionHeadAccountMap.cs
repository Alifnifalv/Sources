using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
{
    [Table("AssetTransactionHeadAccountMaps", Schema = "asset")]
    public partial class AssetTransactionHeadAccountMap
    {
        [Key]
        public long AssetTransactionHeadAccountMapIID { get; set; }

        public long AccountTransactionID { get; set; }

        public long AssetTransactionHeadID { get; set; }

        public virtual AccountTransaction AccountTransaction { get; set; }

        public virtual AssetTransactionHead AssetTransactionHead { get; set; }
    }
}