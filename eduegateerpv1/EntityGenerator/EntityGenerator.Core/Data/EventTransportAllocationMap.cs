using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EventTransportAllocationMap", Schema = "schools")]
    public partial class EventTransportAllocationMap
    {
        [Key]
        public long EventTransportAllocationMapIID { get; set; }
        public long? EventTransportAllocationID { get; set; }
        public long? StudentRouteStopMapID { get; set; }
        public long? StudentID { get; set; }
        public long? StaffRouteStopMapID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(500)]
        public string PickUpRoute { get; set; }
        [StringLength(500)]
        public string DropRoute { get; set; }
        [StringLength(500)]
        public string PickUpStop { get; set; }
        [StringLength(500)]
        public string DropStop { get; set; }
        public int? ToRouteID { get; set; }

        [ForeignKey("EventTransportAllocationID")]
        [InverseProperty("EventTransportAllocationMaps")]
        public virtual EventTransportAllocation EventTransportAllocation { get; set; }
        [ForeignKey("StaffRouteStopMapID")]
        [InverseProperty("EventTransportAllocationMaps")]
        public virtual StaffRouteStopMap StaffRouteStopMap { get; set; }
        [ForeignKey("StudentRouteStopMapID")]
        [InverseProperty("EventTransportAllocationMaps")]
        public virtual StudentRouteStopMap StudentRouteStopMap { get; set; }
        [ForeignKey("ToRouteID")]
        [InverseProperty("EventTransportAllocationMaps")]
        public virtual Route1 ToRoute { get; set; }
    }
}
