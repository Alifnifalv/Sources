using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("Students", Schema = "schools")]
    [Index("AcademicYearID", Name = "IDX_Students_AcademicYearID_AdmissionNumber__FirstName__MiddleName__LastName")]
    [Index("AcademicYearID", Name = "IDX_Students_AcademicYearID_ClassID__IsActive__Status")]
    [Index("AcademicYearID", Name = "IDX_Students_AcademicYearID_ClassID__SectionID__ParentID__IsActive__Status__FeeStartDate")]
    [Index("AdmissionNumber", Name = "IDX_Students_AdmissionNumber_")]
    [Index("ApplicationID", Name = "IDX_Students_ApplicationID_")]
    [Index("ApplicationID", Name = "IDX_Students_ApplicationID_AdmissionNumber")]
    [Index("ClassID", "IsActive", "SchoolID", "AcademicYearID", Name = "IDX_Students_ClassID__IsActive__SchoolID__AcademicYearID_SectionID__ParentID")]
    [Index("ClassID", "SectionID", Name = "IDX_Students_ClassID__SectionID_IsActive__Status__AcademicYearID")]
    [Index("IsActive", "AcademicYearID", Name = "IDX_Students_IsActive_AcademicYearID")]
    [Index("IsActive", "SchoolID", "AcademicYearID", Name = "IDX_Students_IsActive_SchoolID_AcademicYearID")]
    [Index("IsActive", "SchoolID", "Status", Name = "IDX_Students_IsActive__SchoolIDStatus_ParentID")]
    [Index("IsActive", "Status", "AcademicYearID", Name = "IDX_Students_IsActive__Status__AcademicYearID_AdmissionNumber__FirstName__MiddleName__LastName")]
    [Index("IsActive", "Status", "SchoolID", Name = "IDX_Students_IsActive__Status__SchoolID_ClassID__SectionID__GenderID__AcademicYearID__SecondLangID_")]
    [Index("ParentID", Name = "IDX_Students_ParentID_AdmissionNumber__RollNumber__ClassID__GradeID__SectionID__FirstName__MiddleNa")]
    [Index("ParentID", Name = "IDX_Students_ParentID_ClassID__SectionID__AcademicYearID")]
    [Index("ParentID", "IsActive", Name = "IDX_Students_ParentID__IsActive_AdmissionNumber__RollNumber__ClassID__GradeID__SectionID__FirstName")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_Students_SchoolID_AcademicYearID")]
    [Index("SchoolID", Name = "IDX_Students_SchoolID_AdmissionNumber__FirstName__MiddleName__LastName__IsActive__Status")]
    [Index("SchoolID", Name = "IDX_Students_SchoolID_IsActive__Status")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_Students_SchoolID__AcademicYearID_AdmissionNumber__ClassID__SectionID__FirstName__MiddleName__L")]
    [Index("LoginID", Name = "_dta_index_Students_7_1620304932__K32_4_6_73")]
    [Index("SchoolID", Name = "idx_StudentsSchoolID")]
    [Index("SchoolID", Name = "idx_StudentsSchoolIDInclAcademic")]
    [Index("SectionID", "IsActive", Name = "idx_StudentsSectionIDIsActive")]
    [Index("ClassID", "IsActive", Name = "idx_StudentsSectionIDIsActiveIncludeSectionID")]
    [Index("LoginID", Name = "idx_student_LogIn")]
    [Index("IsActive", "AcademicYearID", Name = "idx_studentisActAcademic")]
    public partial class Student
    {
        public Student()
        {
            StudentFeeDues = new HashSet<StudentFeeDue>();
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public long StudentIID { get; set; }

        [StringLength(50)]
        public string AdmissionNumber { get; set; }

        [StringLength(50)]
        public string RollNumber { get; set; }

        public int? ClassID { get; set; }

        public byte? GradeID { get; set; }

        public int? SectionID { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        public byte? GenderID { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? StudentCategoryID { get; set; }

        public byte? CastID { get; set; }

        public byte? RelegionID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string EmailID { get; set; }

        public DateTime? AdmissionDate { get; set; }

        [StringLength(500)]
        public string StudentProfile { get; set; }

        public int? BloodGroupID { get; set; }

        public int? StudentHouseID { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string Height { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string Weight { get; set; }

        public DateTime? AsOnDate { get; set; }

        public int? HostelID { get; set; }

        public long? HostelRoomID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public long? Account_Sub_ledger_ID { get; set; }

        public long? LoginID { get; set; }

        public long? ParentID { get; set; }

        public bool? IsActive { get; set; }
        /// <summary>
        /// 1-Active
        /// 2-Transferred
        /// 3-Discontinue
        /// 
        /// </summary>
        public byte? Status { get; set; }

        [StringLength(20)]
        public string CurrentBuildingNo { get; set; }

        [StringLength(20)]
        public string CurrentFlatNo { get; set; }

        [StringLength(20)]
        public string CurrentStreetNo { get; set; }

        [StringLength(25)]
        public string CurrentStreetName { get; set; }

        [StringLength(20)]
        public string CurrentLocationNo { get; set; }

        [StringLength(25)]
        public string CurrentLocationName { get; set; }

        [StringLength(20)]
        public string CurrentZipNo { get; set; }

        [StringLength(25)]
        public string CurrentCity { get; set; }

        [StringLength(25)]
        public string CurrentPostBoxNo { get; set; }

        public int? CurrentCountryID { get; set; }

        public bool? IsAddressIsCurrentAddress { get; set; }

        [StringLength(20)]
        public string PermenentBuildingNo { get; set; }

        [StringLength(20)]
        public string PermenentFlatNo { get; set; }

        [StringLength(20)]
        public string PermenentStreetNo { get; set; }

        [StringLength(25)]
        public string PermenentStreetName { get; set; }

        [StringLength(20)]
        public string PermenentLocationNo { get; set; }

        [StringLength(25)]
        public string PermenentLocationName { get; set; }

        [StringLength(20)]
        public string PermenentZipNo { get; set; }

        [StringLength(25)]
        public string PermenentCity { get; set; }

        [StringLength(25)]
        public string PermenentPostBoxNo { get; set; }

        public int? PermenentCountryID { get; set; }

        public bool? IsAddressIsPermenentAddress { get; set; }

        public DateTime? FeeStartDate { get; set; }

        public byte? CommunityID { get; set; }

        public bool? IsMinority { get; set; }

        public bool? IsOnlyChildofParent { get; set; }

        public byte? PrimaryContactID { get; set; }

        public int? SecoundLanguageID { get; set; }

        public int? ThridLanguageID { get; set; }

        public bool? IsStudentStudiedBefore { get; set; }

        [StringLength(200)]
        public string PreviousSchoolName { get; set; }

        public byte? PreviousSchoolSyllabusID { get; set; }

        [StringLength(15)]
        public string PreviousSchoolAcademicYear { get; set; }

        public int? PreviousSchoolClassCompletedID { get; set; }

        [StringLength(250)]
        public string PreviousSchoolAddress { get; set; }

        public long? ApplicationID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(500)]
        public string MigratedProfileName { get; set; }

        public int? SubjectMapID { get; set; }

        public int? SchoolAcademicyearID { get; set; }

        public int? SecondLangID { get; set; }

        public int? ThirdLangID { get; set; }

        public byte? StreamID { get; set; }

        public DateTime? InactiveDate { get; set; }

        public virtual Login Login { get; set; }
        
        public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }
        
        public virtual ICollection<Ticket> Tickets { get; set; }        
    }
}