using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EventTransportAllocation", Schema = "schools")]
    public partial class EventTransportAllocation
    {
        public EventTransportAllocation()
        {
            EventTransportAllocationMaps = new HashSet<EventTransportAllocationMap>();
        }

        [Key]
        public long EventTransportAllocationIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventDate { get; set; }
        public int? RouteID { get; set; }
        public long? VehicleID { get; set; }
        public long? DriverID { get; set; }
        public long? AttendarID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public long? EventID { get; set; }
        public bool? IsPickUp { get; set; }

        [ForeignKey("AttendarID")]
        [InverseProperty("EventTransportAllocationAttendars")]
        public virtual Employee Attendar { get; set; }
        [ForeignKey("DriverID")]
        [InverseProperty("EventTransportAllocationDrivers")]
        public virtual Employee Driver { get; set; }
        [ForeignKey("EventID")]
        [InverseProperty("EventTransportAllocations")]
        public virtual SchoolEvent Event { get; set; }
        [ForeignKey("VehicleID")]
        [InverseProperty("EventTransportAllocations")]
        public virtual Vehicle Vehicle { get; set; }
        [InverseProperty("EventTransportAllocation")]
        public virtual ICollection<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }
    }
}
