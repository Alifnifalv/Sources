using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StaffRouteShiftMapLogs", Schema = "schools")]
    public partial class StaffRouteShiftMapLog
    {
        [Key]
        public long StaffRouteStopMapLogIID { get; set; }

        public long? StaffRouteStopMapID { get; set; }

        public long? StaffID { get; set; }

        public long? PickupStopMapID { get; set; }

        public long? DropStopMapID { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? PickupRouteID { get; set; }

        public int? DropStopRouteID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public long? TransportStatusID { get; set; }

        public string Remarks { get; set; }

        public int? IsRouteShifted { get; set; }

        public int? ShiftFromRouteID { get; set; }

        [StringLength(500)]
        public string OldPickUpStop { get; set; }

        [StringLength(500)]
        public string OldDropStop { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Routes1 Routes1 { get; set; }

        public virtual RouteStopMap RouteStopMap { get; set; }

        public virtual RouteStopMap RouteStopMap1 { get; set; }

        public virtual Schools School { get; set; }

        public virtual StaffRouteStopMap StaffRouteStopMap { get; set; }

        public virtual TransportStatus TransportStatus { get; set; }
    }
}
