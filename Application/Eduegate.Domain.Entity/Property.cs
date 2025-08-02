namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.Properties")]
    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(1000)]
        public string Expression { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductFamilyPropertyMap> ProductFamilyPropertyMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPropertyMap> ProductPropertyMaps { get; set; }

        public virtual UIControlType UIControlType { get; set; }

        public virtual UIControlValidation UIControlValidation { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyCultureData> PropertyCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyType> PropertyTypes { get; set; }
    }
}
