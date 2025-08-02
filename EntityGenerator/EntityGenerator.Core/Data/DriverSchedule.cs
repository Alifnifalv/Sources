using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DriverSchedules", Schema = "schedule")]
    public partial class DriverSchedule
    {
        [Key]
        public long DriverScheduleIdty { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ScheduleDate { get; set; }
        public long? ScheduleID { get; set; }
        public long? DriverID { get; set; }
        public long? MiadID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Pickup { get; set; }
        [StringLength(500)]
        public string PickupLocation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DropOff { get; set; }
        [StringLength(500)]
        public string DropLocation { get; set; }
        public long? AreaID { get; set; }
        public int? StatusID { get; set; }
        public long? ExternalReferenceID1 { get; set; }
        public long? ExternalReferenceID2 { get; set; }
    }
}
