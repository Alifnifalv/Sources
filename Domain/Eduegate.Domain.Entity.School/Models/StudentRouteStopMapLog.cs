using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StudentRouteStopMapLogs", Schema = "schools")]
    public partial class StudentRouteStopMapLog
    {
        [Key]
        public long StudentRouteStopMapLogIID { get; set; }

        public long? StudentRouteStopMapID { get; set; }

        public long? StudentID { get; set; }

        public long? RouteStopMapID { get; set; }

        public long? PickupStopMapID { get; set; }

        public long? DropStopMapID { get; set; }

        public bool? IsOneWay { get; set; }

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

        public bool? Termsandco { get; set; }

        [StringLength(20)]
        public string StudentRouteStopCode { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public long? TransportStatusID { get; set; }

        public string Remarks { get; set; }

        public int? SectionID { get; set; }

        public int? ClassID { get; set; }

        public DateTime? PickupTime { get; set; }

        public DateTime? DropTime { get; set; }

        public int? IsRouteShifted { get; set; }

        public int? RouteID { get; set; }

        public string OldPickUpStop { get; set; }

        public string OldDropStop { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual Routes1 Routes1 { get; set; }

        public virtual Routes1 Routes11 { get; set; }

        public virtual Routes1 Routes12 { get; set; }

        public virtual RouteStopMap RouteStopMap { get; set; }

        public virtual RouteStopMap RouteStopMap1 { get; set; }

        public virtual RouteStopMap RouteStopMap2 { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }

        public virtual StudentRouteStopMap StudentRouteStopMap { get; set; }

        public virtual TransportStatus TransportStatus { get; set; }
    }
}
