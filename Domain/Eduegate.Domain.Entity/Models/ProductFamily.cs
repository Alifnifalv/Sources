using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductFamilies", Schema = "catalog")]
    public partial class ProductFamily
    {
        public ProductFamily()
        {
            this.ProductFamilyCultureDatas = new List<ProductFamilyCultureData>();
            this.ProductFamilyPropertyMaps = new List<ProductFamilyPropertyMap>();
            this.Products = new List<Product>();
        }

        [Key]
        public long ProductFamilyIID { get; set; }
        public string FamilyName { get; set; }
        public Nullable<int> ProductFamilyTypeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }
        public virtual ICollection<ProductFamilyPropertyMap> ProductFamilyPropertyMaps { get; set; }
        public virtual ICollection<ProductFamilyPropertyTypeMap> ProductFamilyPropertyTypeMaps { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
