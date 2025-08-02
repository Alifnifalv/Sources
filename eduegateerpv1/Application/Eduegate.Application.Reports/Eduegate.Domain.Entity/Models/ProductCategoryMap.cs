using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("catalog.ProductCategoryMaps")]
    public partial class ProductCategoryMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductCategoryMap()
        {
            FeeDueInventoryMaps = new HashSet<FeeDueInventoryMap>();
        }

        [Key]
        public long ProductCategoryMapIID { get; set; }

        public long? ProductID { get; set; }

        public long? CategoryID { get; set; }

        public bool? IsPrimary { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        //public byte[] TimeStamps { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }

        public virtual Product Product { get; set; }

    }
}