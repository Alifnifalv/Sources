using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("HostelTypes", Schema = "schools")]
    public partial class HostelType
    {
        public HostelType()
        {
            Hostels = new HashSet<Hostel>();
        }

        [Key]
        public byte HostelTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("HostelType")]
        public virtual ICollection<Hostel> Hostels { get; set; }
    }
}
