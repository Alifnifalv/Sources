namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VisitingPurposes", Schema = "schools")]
    public partial class VisitingPurpose
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VisitingPurpose()
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
