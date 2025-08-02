using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TransportApplicationsView
    {
        public long TransportApplicationIID { get; set; }
        [StringLength(50)]
        public string ApplicationNumber { get; set; }
        [Required]
        [StringLength(15)]
        [Unicode(false)]
        public string IsNewRider { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        public long? LoginID { get; set; }
        public long? ParentID { get; set; }
        [StringLength(200)]
        public string LandMark { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        public string MotherContactNumber { get; set; }
        [StringLength(50)]
        public string MotherEmailID { get; set; }
        [StringLength(50)]
        public string StreetNo { get; set; }
        [StringLength(50)]
        public string StreetName { get; set; }
        [StringLength(50)]
        public string LocationNo { get; set; }
        [StringLength(50)]
        public string LocationName { get; set; }
        [StringLength(50)]
        public string ZoneNo { get; set; }
        public short? ZoneID { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string EmergencyContactNumber { get; set; }
        [StringLength(50)]
        public string EmergencyEmailID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PickUpTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DropOffTime { get; set; }
        [StringLength(75)]
        public string PickUpStop { get; set; }
        [StringLength(75)]
        public string DropOffStop { get; set; }
        public short? StreetID { get; set; }
        [StringLength(50)]
        public string FatherContactNumber { get; set; }
        [StringLength(50)]
        public string FatherEmailID { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public long TransportApplctnStudentMapIID { get; set; }
        public long? TransportApplicationID { get; set; }
        public int? ClassID { get; set; }
        public byte? TransportApplcnStatusID { get; set; }
        public bool? IsMedicalCondition { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [Column("Building/FlatNo")]
        [StringLength(20)]
        public string Building_FlatNo { get; set; }
        public long? StudentID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(103)]
        public string ClassSection { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(50)]
        public string FatherMobileNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MotherMobileNumber { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [StringLength(50)]
        public string PickUpStop1 { get; set; }
        [StringLength(100)]
        public string PickUpRoute { get; set; }
        [StringLength(50)]
        public string DropStop { get; set; }
        [StringLength(100)]
        public string DropRoute { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsRouteDifferent { get; set; }
    }
}
