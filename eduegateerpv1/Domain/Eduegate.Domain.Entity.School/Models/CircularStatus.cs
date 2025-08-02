using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.School.Models
{
    [Table("CircularStatuses", Schema = "schools")]
    public partial class CircularStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CircularStatus()
        {
            Circulars = new HashSet<Circular>();
        }
        [Key]
        public byte CircularStatusID { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Circular> Circulars { get; set; }
    }
}