namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.VisitingPurposes")]
    public partial class VisitingPurpos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VisitingPurpos()
        {
            VisitorBooks = new HashSet<VisitorBook>();
        }

        [Key]
        public byte VisitingPurposeID { get; set; }

        [StringLength(100)]
        public string PurpuseDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitorBook> VisitorBooks { get; set; }
    }
}
