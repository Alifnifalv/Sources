using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentDetailedAdvanceSearchView
    {
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long StudentIID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [Required]
        [StringLength(502)]
        public string FirstName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [StringLength(100)]
        public string BloodGroupName { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        [StringLength(20)]
        public string StudentPassport { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        [Required]
        [StringLength(302)]
        public string GaurdianName { get; set; }
        [StringLength(50)]
        public string GuardianPhone { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [StringLength(50)]
        public string ApplicationNumber { get; set; }
        [StringLength(302)]
        public string FatherName { get; set; }
        [StringLength(302)]
        public string MotherName { get; set; }
        [StringLength(152)]
        public string FilteredParentMobileNumber { get; set; }
        [Required]
        [StringLength(1108)]
        public string FilteredParentName { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
    }
}
