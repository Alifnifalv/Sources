namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.PresentStatuses")]
    public partial class PresentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PresentStatus()
        {
            MarkRegisters = new HashSet<MarkRegister>();
            StudentAttendences = new HashSet<StudentAttendence>();
        }

        public byte PresentStatusID { get; set; }

        [StringLength(50)]
        public string StatusDescription { get; set; }

        [StringLength(10)]
        public string StatusTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
    }
}
