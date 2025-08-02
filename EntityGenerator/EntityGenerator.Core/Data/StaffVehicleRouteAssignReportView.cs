using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StaffVehicleRouteAssignReportView
    {
        public long StaffRouteStopMapIID { get; set; }
        public long? StaffID { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        public long? PickupStopMapID { get; set; }
        [StringLength(50)]
        public string PickupStopName { get; set; }
        public long? DropStopMapID { get; set; }
        [StringLength(50)]
        public string DropStopName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string IsOneWay { get; set; }
    }
}
