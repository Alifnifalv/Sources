using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StaffTransportDetailReportView
    {
        public long StaffRouteStopMapIID { get; set; }
        public long? StaffID { get; set; }
        [Required]
        [StringLength(50)]
        public string StaffCode { get; set; }
        [StringLength(502)]
        public string StaffName { get; set; }
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
        public bool? TermsandConditions { get; set; }
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
