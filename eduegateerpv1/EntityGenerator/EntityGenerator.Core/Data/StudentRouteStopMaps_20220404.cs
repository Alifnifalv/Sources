using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentRouteStopMaps_20220404
    {
        public long StudentRouteStopMapIID { get; set; }
        public long? StudentID { get; set; }
        public long? RouteStopMapID { get; set; }
        public long? PickupStopMapID { get; set; }
        public long? DropStopMapID { get; set; }
        public bool? IsOneWay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
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
        [Column(TypeName = "datetime")]
        public DateTime? PickupTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DropTime { get; set; }
        public int? IsRouteShifted { get; set; }
    }
}
