namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.PageBoilerplateMaps")]
    public partial class PageBoilerplateMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PageBoilerplateMap()
        {
            CategoryPageBoilerPlatMaps = new HashSet<CategoryPageBoilerPlatMap>();
            PageBoilerplateMapParameters = new HashSet<PageBoilerplateMapParameter>();
        }

        [Key]
        public long PageBoilerplateMapIID { get; set; }

        public long? PageID { get; set; }

        public long? ReferenceID { get; set; }

        public long? BoilerplateID { get; set; }

        public int? SerialNumber { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual BoilerPlate BoilerPlate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryPageBoilerPlatMap> CategoryPageBoilerPlatMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageBoilerplateMapParameter> PageBoilerplateMapParameters { get; set; }

        public virtual Page Page { get; set; }
    }
}
