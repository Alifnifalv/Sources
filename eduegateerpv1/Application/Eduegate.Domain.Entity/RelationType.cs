namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.RelationTypes")]
    public partial class RelationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RelationType()
        {
            EmployeeCatalogRelations = new HashSet<EmployeeCatalogRelation>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RelationTypeID { get; set; }

        [StringLength(100)]
        public string RelationName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeCatalogRelation> EmployeeCatalogRelations { get; set; }
    }
}
