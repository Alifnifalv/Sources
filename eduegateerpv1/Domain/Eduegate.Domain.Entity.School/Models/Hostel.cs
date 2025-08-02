namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Hostels", Schema = "schools")]
    public partial class Hostel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hostel()
        {
            HostelRooms = new HashSet<HostelRoom>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HostelID { get; set; }

        [StringLength(50)]
        public string HostelName { get; set; }

        public byte? HostelTypeID { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public int? InTake { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HostelRoom> HostelRooms { get; set; }

        public virtual HostelType HostelType { get; set; }
    }
}
