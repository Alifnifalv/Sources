using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UnitGroups", Schema = "catalog")]
    public partial class UnitGroup
    {
        public UnitGroup()
        {
            ProductPriceListSKUMaps = new HashSet<ProductPriceListSKUMap>();
            ProductPurchaseUnitGroups = new HashSet<Product>();
            ProductSellingUnitGroups = new HashSet<Product>();
            ProductUnitGroups = new HashSet<Product>();
            Units = new HashSet<Unit>();
        }

        [Key]
        public long UnitGroupID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string UnitGroupCode { get; set; }
        [StringLength(50)]
        public string UnitGroupName { get; set; }
        public double? Fraction { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("UnitGround")]
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        [InverseProperty("PurchaseUnitGroup")]
        public virtual ICollection<Product> ProductPurchaseUnitGroups { get; set; }
        [InverseProperty("SellingUnitGroup")]
        public virtual ICollection<Product> ProductSellingUnitGroups { get; set; }
        [InverseProperty("UnitGroup")]
        public virtual ICollection<Product> ProductUnitGroups { get; set; }
        [InverseProperty("UnitGroup")]
        public virtual ICollection<Unit> Units { get; set; }
    }
}
