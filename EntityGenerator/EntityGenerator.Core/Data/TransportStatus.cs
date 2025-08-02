using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransportStatuses", Schema = "schools")]
    public partial class TransportStatus
    {
        public TransportStatus()
        {
            StaffRouteShiftMapLogs = new HashSet<StaffRouteShiftMapLog>();
            StaffRouteStopMaps = new HashSet<StaffRouteStopMap>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
        }

        [Key]
        public long TransportStatusID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }

        [InverseProperty("TransportStatus")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }
        [InverseProperty("TransportStatus")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }
        [InverseProperty("TransportStatus")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }
        [InverseProperty("TransportStatus")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
    }
}
