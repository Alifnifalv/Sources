using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RoomTypes", Schema = "schools")]
    public partial class RoomType
    {
        public RoomType()
        {
            HostelRooms = new HashSet<HostelRoom>();
        }

        [Key]
        public int RoomTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("RoomType")]
        public virtual ICollection<HostelRoom> HostelRooms { get; set; }
    }
}
