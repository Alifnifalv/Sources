using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetTransactionDetails", Schema = "asset")]
    public partial class AssetTransactionDetail
    {
        [Key]
        public long DetailIID { get; set; }
        public long? HeadID { get; set; }
        public long? AssetID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        public int? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? AccountID { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("AssetTransactionDetails")]
        public virtual Account Account { get; set; }
        [ForeignKey("AssetID")]
        [InverseProperty("AssetTransactionDetails")]
        public virtual Asset Asset { get; set; }
        [ForeignKey("HeadID")]
        [InverseProperty("AssetTransactionDetails")]
        public virtual AssetTransactionHead Head { get; set; }
    }
}
