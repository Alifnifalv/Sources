namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Building Building { get; set; }
    }
}
