using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductFamilies", Schema = "catalog")]
    public partial class ProductFamily
    {
        public ProductFamily()
        {
            ProductFamilyCultureDatas = new HashSet<ProductFamilyCultureData>();
            ProductFamilyPropertyMaps = new HashSet<ProductFamilyPropertyMap>();
            ProductFamilyPropertyTypeMaps = new HashSet<ProductFamilyPropertyTypeMap>();
            Products = new HashSet<Product>();
        }

        [Key]
        public long ProductFamilyIID { get; set; }
        [StringLength(100)]
        public string FamilyName { get; set; }
        public int? ProductFamilyTypeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("ProductFamily")]
        public virtual ICollection<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }
        [InverseProperty("ProductFamily")]
        public virtual ICollection<ProductFamilyPropertyMap> ProductFamilyPropertyMaps { get; set; }
        [InverseProperty("ProductFamily")]
        public virtual ICollection<ProductFamilyPropertyTypeMap> ProductFamilyPropertyTypeMaps { get; set; }
        [InverseProperty("ProductFamily")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
