using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Units", Schema = "catalog")]
    public partial class Unit
    {
        public Unit()
        {
            Assets = new HashSet<Asset>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            ProductPurchaseUnits = new HashSet<Product>();
            ProductSellingUnits = new HashSet<Product>();
            ProductUnits = new HashSet<Product>();
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        [Key]
        public long UnitID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string UnitCode { get; set; }
        [StringLength(50)]
        public string UnitName { get; set; }
        public double? Fraction { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(20)]
        public string ShortName { get; set; }
        public long? UnitGroupID { get; set; }

        [ForeignKey("UnitGroupID")]
        [InverseProperty("Units")]
        public virtual UnitGroup UnitGroup { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<Asset> Assets { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        [InverseProperty("PurchaseUnit")]
        public virtual ICollection<Product> ProductPurchaseUnits { get; set; }
        [InverseProperty("SellingUnit")]
        public virtual ICollection<Product> ProductSellingUnits { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<Product> ProductUnits { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
