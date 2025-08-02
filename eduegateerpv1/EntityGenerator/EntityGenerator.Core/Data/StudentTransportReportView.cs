using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentTransportReportView
    {
        public long StudentRouteStopMapIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public long? PickUpStopMapID { get; set; }
        [StringLength(50)]
        public string PickUpStopName { get; set; }
        public long? DropStopMapID { get; set; }
        [StringLength(50)]
        public string DropStopName { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string IsOneWay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public bool? IsActive { get; set; }
        public int? PickUpRouteID { get; set; }
        [StringLength(153)]
        public string PickUpRoute { get; set; }
        [StringLength(153)]
        public string DropRoute { get; set; }
        public int? DropStopRouteID { get; set; }
        public bool? Termsandco { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(125)]
        public string AcademicYear { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public long? TransportStatusID { get; set; }
        [StringLength(100)]
        public string TransportStatus { get; set; }
        public long? PickVehicleID { get; set; }
        public long? DropVehiclID { get; set; }
    }
}
