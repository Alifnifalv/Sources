using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Properties", Schema = "catalog")]
    public partial class Property
    {
        public Property()
        {
            ProductFamilyPropertyMaps = new HashSet<ProductFamilyPropertyMap>();
            ProductPropertyMaps = new HashSet<ProductPropertyMap>();
            PropertyCultureDatas = new HashSet<PropertyCultureData>();
            PropertyTypes = new HashSet<PropertyType>();
        }

        [Key]
        public long PropertyIID { get; set; }
        [StringLength(50)]
        public string PropertyName { get; set; }
        [StringLength(100)]
        public string PropertyDescription { get; set; }
        [StringLength(50)]
        public string DefaultValue { get; set; }
        public byte? PropertyTypeID { get; set; }
        public bool? IsUnqiue { get; set; }
        public byte? UIControlTypeID { get; set; }
        public byte? UIControlValidationID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(1000)]
        public string Expression { get; set; }

        [ForeignKey("PropertyTypeID")]
        [InverseProperty("Properties")]
        public virtual PropertyType PropertyType { get; set; }
        [ForeignKey("UIControlTypeID")]
        [InverseProperty("Properties")]
        public virtual UIControlType UIControlType { get; set; }
        [ForeignKey("UIControlValidationID")]
        [InverseProperty("Properties")]
        public virtual UIControlValidation UIControlValidation { get; set; }
        [InverseProperty("ProductProperty")]
        public virtual ICollection<ProductFamilyPropertyMap> ProductFamilyPropertyMaps { get; set; }
        [InverseProperty("Property")]
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        [InverseProperty("Property")]
        public virtual ICollection<PropertyCultureData> PropertyCultureDatas { get; set; }

        [ForeignKey("PropertyID")]
        [InverseProperty("PropertiesNavigation")]
        public virtual ICollection<PropertyType> PropertyTypes { get; set; }
    }
}
