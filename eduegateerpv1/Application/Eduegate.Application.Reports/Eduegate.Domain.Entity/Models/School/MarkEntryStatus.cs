namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.MarkEntryStatuses")]
    public partial class MarkEntryStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MarkEntryStatus()
        {
            MarkRegisters = new HashSet<MarkRegister>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte MarkEntryStatusID { get; set; }

        [StringLength(50)]
        public string MarkEntryStatusName { get; set; }

        [StringLength(10)]
        public string MarkEntryCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
    }
}
