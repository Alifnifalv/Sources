using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentRouteStopMapsSearchView
    {
        public long StudentRouteStopMapIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public long? PickupStopMapID { get; set; }
        [StringLength(50)]
        public string PickupStopName { get; set; }
        [StringLength(20)]
        public string PickupVehicleCode { get; set; }
        public long? DropStopMapID { get; set; }
        [StringLength(50)]
        public string DropStopName { get; set; }
        [StringLength(20)]
        public string DropVehicleCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(103)]
        public string ClassSection { get; set; }
        [StringLength(12)]
        [Unicode(false)]
        public string TransportStatus { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string IsOneWay { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(50)]
        public string FatherMobileNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MotherMobileNumber { get; set; }
        [StringLength(100)]
        public string PickUpRouteCode { get; set; }
        [StringLength(100)]
        public string DropRouteCode { get; set; }
        [StringLength(100)]
        public string AcademicYear { get; set; }
    }
}
