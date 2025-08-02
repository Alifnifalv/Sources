using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class vDriverSchedule
    {
        public long DriverScheduleIdty { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ScheduleDate { get; set; }
        public long? ScheduleId { get; set; }
        public long? DriverId { get; set; }
        [StringLength(20)]
        public string DriverCode { get; set; }
        [StringLength(50)]
        public string DriverName { get; set; }
        public long? MaidId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PickUp { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string PickUp_Location { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DropOff { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string DropOff_Location { get; set; }
        public long? AreaId { get; set; }
        [Required]
        [StringLength(20)]
        public string MaidCode { get; set; }
        [Required]
        [StringLength(50)]
        public string MaidName { get; set; }
        public int StatusID { get; set; }
    }
}
