using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Students", Schema = "schools")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            TransactionHeads = new HashSet<TransactionHead>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            ShoppingCarts = new HashSet<ShoppingCart>();
            AllergyStudentMaps = new HashSet<AllergyStudentMap>();
            StudentTransferRequests = new HashSet<StudentTransferRequest>();
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

        public long? Account_Sub_ledger_ID { get; set; }

        public long? LoginID { get; set; }

        public long? ParentID { get; set; }

        public bool? IsActive { get; set; }

        public byte? Status { get; set; }

        public long? ApplicationID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual Parent Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public virtual Section Section { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllergyStudentMap> AllergyStudentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentTransferRequest> StudentTransferRequests { get; set; }

        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

    }
}