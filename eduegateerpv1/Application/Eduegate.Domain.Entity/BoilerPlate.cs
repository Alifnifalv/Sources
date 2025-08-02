namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.BoilerPlates")]
    public partial class BoilerPlate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoilerPlate()
        {
            BoilerPlateParameters = new HashSet<BoilerPlateParameter>();
            PageBoilerplateMaps = new HashSet<PageBoilerplateMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long BoilerPlateID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Template { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(50)]
        public string ReferenceIDName { get; set; }

        public bool? ReferenceIDRequired { get; set; }

        public int? CompanyID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoilerPlateParameter> BoilerPlateParameters { get; set; }

        public virtual BoilerPlate BoilerPlates1 { get; set; }

        public virtual BoilerPlate BoilerPlate1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageBoilerplateMap> PageBoilerplateMaps { get; set; }
    }
}
