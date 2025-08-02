using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SeatAvailabilityListView
    {
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        public string VehicleRegistrationNumber { get; set; }
        [StringLength(20)]
        public string VehicleCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int? RouteID { get; set; }
        [StringLength(100)]
        public string RouteCode { get; set; }
        [StringLength(50)]
        public string ModelName { get; set; }
        public int? AllowSeatingCapacity { get; set; }
        public int? MaximumSeatingCapacity { get; set; }
        [StringLength(100)]
        public string FleetCode { get; set; }
        public int? PickupOccupied { get; set; }
        public int? DropOccupied { get; set; }
        public int? StaffPickupOccupied { get; set; }
        public int? StaffDropOccupied { get; set; }
        public int? TotalPickUP { get; set; }
        public int? TotalDrop { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
