using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentWiseReportView
    {
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]
        public string RelegionName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(2000)]
        public string PermanentAddress { get; set; }
        [StringLength(20)]
        public string PassportNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportNoExpiry { get; set; }
        [StringLength(20)]
        public string VisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaExpiry { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NationalIDNoExpiry { get; set; }
        [StringLength(50)]
        public string CountryName { get; set; }
        public int? NationalityID { get; set; }
        public int? CountryofIssueID { get; set; }
        public int? CountryofBirthID { get; set; }
        [StringLength(20)]
        public string FatherFirstName { get; set; }
        [StringLength(20)]
        public string FatherMiddleName { get; set; }
        [StringLength(20)]
        public string FatherLastName { get; set; }
        [StringLength(20)]
        public string MotherFirstName { get; set; }
        [StringLength(20)]
        public string MotherMiddleName { get; set; }
        [StringLength(20)]
        public string MotherLastName { get; set; }
        [StringLength(20)]
        public string FatherOccupation { get; set; }
        [StringLength(25)]
        public string PreviousSchoolName { get; set; }
        [StringLength(15)]
        public string MotherMobileNumber { get; set; }
        [StringLength(25)]
        public string MotherEmailID { get; set; }
        [StringLength(20)]
        public string MotherOccupation { get; set; }
        public int? PreviousSchoolClassCompletedID { get; set; }
        [StringLength(15)]
        public string PreviousSchoolAcademicYear { get; set; }
        public byte? PreviousSchoolSyllabusID { get; set; }
        [StringLength(50)]
        public string SyllabusDescription { get; set; }
        [StringLength(100)]
        public string Expr1 { get; set; }
    }
}
