using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BuildingClassRoomMaps", Schema = "schools")]
    public partial class BuildingClassRoomMap
    {
        [Key]
        public long BuildingClassRoomMapIID { get; set; }
        public int? BuildingID { get; set; }
        [StringLength(50)]
        public string RoomName { get; set; }
        public int? Capacity { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BuildingID")]
        [InverseProperty("BuildingClassRoomMaps")]
        public virtual Building Building { get; set; }
    }
}
