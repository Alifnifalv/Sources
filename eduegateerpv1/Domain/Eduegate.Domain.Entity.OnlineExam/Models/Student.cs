using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("Students", Schema = "schools")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Candidates = new HashSet<Candidate>();
            StudentPassportDetails = new HashSet<StudentPassportDetail>();
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
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string EmailID { get; set; }

        public DateTime? AdmissionDate { get; set; }

        [StringLength(500)]
        public string StudentProfile { get; set; }

        public int? BloodGroupID { get; set; }

        public int? StudentHouseID { get; set; }

        [StringLength(20)]
        public string Height { get; set; }

        [StringLength(20)]
        public string Weight { get; set; }

        public DateTime? AsOnDate { get; set; }

        public int? HostelID { get; set; }

        public long? HostelRoomID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? Account_Sub_ledger_ID { get; set; }

        public long? LoginID { get; set; }

        public long? ParentID { get; set; }

        public bool? IsActive { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Candidate> Candidates { get; set; }

        //public virtual AcademicYear AcademicYear { get; set; }

        //public virtual AcademicYear AcademicYear1 { get; set; }

        //public virtual Class Class { get; set; }

        //public virtual Class Class1 { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        //public virtual StudentApplication StudentApplication { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPassportDetail> StudentPassportDetails { get; set; }

        //public virtual Subject Subject { get; set; }

        //public virtual Subject Subject1 { get; set; }

        //public virtual Subject Subject2 { get; set; }
    }
}