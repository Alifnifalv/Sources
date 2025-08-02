using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("BreakTypes", Schema = "schools")]
    public partial class BreakType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BreakType()
        {
            ClassTimings = new HashSet<ClassTiming>();
        }
        [Key]
        public byte BreakTypeID { get; set; }

        [StringLength(50)]
        public string BreakTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassTiming> ClassTimings { get; set; }
    }
}