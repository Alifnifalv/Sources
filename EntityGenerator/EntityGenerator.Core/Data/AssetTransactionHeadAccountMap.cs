using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetTransactionHeadAccountMaps", Schema = "asset")]
    public partial class AssetTransactionHeadAccountMap
    {
        [Key]
        public long AssetTransactionHeadAccountMapIID { get; set; }
        public long AccountTransactionID { get; set; }
        public long AssetTransactionHeadID { get; set; }

        [ForeignKey("AccountTransactionID")]
        [InverseProperty("AssetTransactionHeadAccountMaps")]
        public virtual AccountTransaction AccountTransaction { get; set; }
        [ForeignKey("AssetTransactionHeadID")]
        [InverseProperty("AssetTransactionHeadAccountMaps")]
        public virtual AssetTransactionHead AssetTransactionHead { get; set; }
    }
}
