using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("Sections", Schema = "schools")]
    public partial class Section
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Section()
        {
            Students = new HashSet<Student>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionID { get; set; }

        [StringLength(50)]
        public string SectionName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("microsoft.usage", "ca2227:collectionpropertiesshouldbereadonly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}