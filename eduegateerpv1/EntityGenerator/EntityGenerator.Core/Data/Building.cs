using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Buildings", Schema = "schools")]
    public partial class Building
    {
        public Building()
        {
            BuildingClassRoomMaps = new HashSet<BuildingClassRoomMap>();
        }

        [Key]
        public int BuildingID { get; set; }
        [StringLength(50)]
        public string BuildingName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        [InverseProperty("Building")]
        public virtual ICollection<BuildingClassRoomMap> BuildingClassRoomMaps { get; set; }
    }
}
