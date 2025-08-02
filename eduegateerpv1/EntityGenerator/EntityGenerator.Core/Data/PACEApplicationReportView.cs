using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PACEApplicationReportView
    {
        public long? ReferenceIID { get; set; }
        public string AdmissionNumber { get; set; }
        public string StudentName { get; set; }
        public string Grade { get; set; }
        public string SchoolName { get; set; }
        public string AcademicYearName { get; set; }
        public string Curriculam { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public string BirthCountry { get; set; }
        public string Nationality { get; set; }
        public string SecondLanguage { get; set; }
        public string ThirdLanguage { get; set; }
        public string PassportNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportExpiryDate { get; set; }
        public string NationalID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NationalIDExpiryDate { get; set; }
        public string PrimaryContact { get; set; }
        public string POBoxNumber { get; set; }
        public string PrimaryAddress { get; set; }
        public string FatherName { get; set; }
        public string FatherMobileNumber { get; set; }
        public string FatherDesignation { get; set; }
        public string FatherCompanyName { get; set; }
        public string FatherNationalID { get; set; }
        public string FatherEmailID { get; set; }
        public string MotherName { get; set; }
        public string MotherMobileNumber { get; set; }
        public string MotherDesignation { get; set; }
        public string MotherCompanyName { get; set; }
        public string MotherNationalID { get; set; }
        public string MotherEmailID { get; set; }
        public string SiblingAdmissionNumber { get; set; }
        public string SiblingName { get; set; }
        public string SiblingGrade { get; set; }
        public string PreviousSchoolName { get; set; }
        public string PreviousSchoolCurriculam { get; set; }
        public string PreviousSchoolGrade { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}
