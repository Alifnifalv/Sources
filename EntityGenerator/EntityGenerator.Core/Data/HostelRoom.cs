using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("HostelRooms", Schema = "schools")]
    public partial class HostelRoom
    {
        public HostelRoom()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public long HostelRoomIID { get; set; }
        [StringLength(50)]
        public string RoomNumber { get; set; }
        public int? HostelID { get; set; }
        public int? RoomTypeID { get; set; }
        public int? NumberOfBed { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPerBed { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("HostelID")]
        [InverseProperty("HostelRooms")]
        public virtual Hostel Hostel { get; set; }
        [ForeignKey("RoomTypeID")]
        [InverseProperty("HostelRooms")]
        public virtual RoomType RoomType { get; set; }
        [InverseProperty("HostelRoom")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
