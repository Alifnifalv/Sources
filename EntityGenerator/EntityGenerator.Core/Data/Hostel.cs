using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Hostels", Schema = "schools")]
    public partial class Hostel
    {
        public Hostel()
        {
            HostelRooms = new HashSet<HostelRoom>();
            Students = new HashSet<Student>();
        }

        [Key]
        public int HostelID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string HostelName { get; set; }
        public byte? HostelTypeID { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        public int? InTake { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [ForeignKey("HostelTypeID")]
        [InverseProperty("Hostels")]
        public virtual HostelType HostelType { get; set; }
        [InverseProperty("Hostel")]
        public virtual ICollection<HostelRoom> HostelRooms { get; set; }
        [InverseProperty("Hostel")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
