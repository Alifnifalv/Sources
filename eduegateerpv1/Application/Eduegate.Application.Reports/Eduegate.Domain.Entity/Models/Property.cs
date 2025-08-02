using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Property
    {
        public Property()
        {
            this.ProductFamilyPropertyMaps = new List<ProductFamilyPropertyMap>();
            //this.ProductImageMaps = new List<ProductImageMap>();
            this.ProductPropertyMaps = new List<ProductPropertyMap>();
            this.PropertyCultureDatas = new List<PropertyCultureData>();
        }

        public long PropertyIID { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDescription { get; set; }
        public string DefaultValue { get; set; }
        public Nullable<byte> PropertyTypeID { get; set; }
        public Nullable<bool> IsUnqiue { get; set; }
        public Nullable<byte> UIControlTypeID { get; set; }
        public Nullable<byte> UIControlValidationID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<ProductFamilyPropertyMap> ProductFamilyPropertyMaps { get; set; }
        //public virtual ICollection<ProductImageMap> ProductImageMaps { get; set; }
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public virtual UIControlType UIControlType { get; set; }
        public virtual UIControlValidation UIControlValidation { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual ICollection<PropertyCultureData> PropertyCultureDatas { get; set; }
    }
}
