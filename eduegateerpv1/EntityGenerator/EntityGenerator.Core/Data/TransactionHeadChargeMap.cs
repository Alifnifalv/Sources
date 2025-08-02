using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionHeadChargeMaps", Schema = "inventory")]
    public partial class TransactionHeadChargeMap
    {
        [Key]
        public long TransactionHeadChargeMapIID { get; set; }
        public long? HeadID { get; set; }
        public int? CartChargeID { get; set; }
        public bool? IsDeduction { get; set; }
        public byte? CartChargeTypeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Percentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey("CartChargeID")]
        [InverseProperty("TransactionHeadChargeMaps")]
        public virtual CartCharge CartCharge { get; set; }
        [ForeignKey("CartChargeTypeID")]
        [InverseProperty("TransactionHeadChargeMaps")]
        public virtual CartChargeType CartChargeType { get; set; }
        [ForeignKey("HeadID")]
        [InverseProperty("TransactionHeadChargeMaps")]
        public virtual TransactionHead Head { get; set; }
    }
}
