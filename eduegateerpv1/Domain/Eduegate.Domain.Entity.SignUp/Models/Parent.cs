using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.SignUp.Models
{
    [Table("Parents", Schema = "schools")]
    public partial class Parent
    {
        public Parent()
        {
            MeetingRequests = new HashSet<MeetingRequest>();
            SignupAudienceMaps = new HashSet<SignupAudienceMap>();
            SignupSlotAllocationMaps = new HashSet<SignupSlotAllocationMap>();
            Students = new HashSet<Student>();
        }

        [Key]
        public long ParentIID { get; set; }

        [StringLength(500)]
        public string FatherName { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string FatherOccupation { get; set; }

        [StringLength(500)]
        public string FatherProfile { get; set; }

        [StringLength(500)]
        public string MotherName { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string MotherPhone { get; set; }

        [StringLength(50)]
        public string MotherOccupation { get; set; }

        [StringLength(500)]
        public string MotherPofile { get; set; }

        public byte? GuardianTypeID { get; set; }

        [StringLength(500)]
        public string GuardianName { get; set; }

        [StringLength(50)]
        public string GuardianRelation { get; set; }

        [StringLength(500)]
        public string GaurdianEmail { get; set; }

        [StringLength(500)]
        public string GuardianPhoto { get; set; }

        [StringLength(50)]
        public string GuardianPhone { get; set; }

        [StringLength(50)]
        public string GuardianOccupation { get; set; }

        [StringLength(2000)]
        public string GuardianAddress { get; set; }

        public long? LoginID { get; set; }

        [StringLength(100)]
        public string BuildingNo { get; set; }

        [StringLength(100)]
        public string FlatNo { get; set; }

        [StringLength(100)]
        public string StreetNo { get; set; }

        [StringLength(100)]
        public string StreetName { get; set; }

        [StringLength(100)]
        public string LocationNo { get; set; }

        [StringLength(100)]
        public string LocationName { get; set; }

        [StringLength(100)]
        public string ZipNo { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string PostBoxNo { get; set; }

        public int? CountryID { get; set; }

        [StringLength(100)]
        public string FatherFirstName { get; set; }

        [StringLength(100)]
        public string FatherMiddleName { get; set; }

        [StringLength(100)]
        public string FatherLastName { get; set; }

        public int? FatherCountryID { get; set; }

        [StringLength(100)]
        public string FatherPassportNumber { get; set; }

        public DateTime? FatherPassportNoIssueDate { get; set; }

        public DateTime? FatherPassportNoExpiryDate { get; set; }

        public int? FatherPassportCountryofIssueID { get; set; }

        [StringLength(100)]
        public string FatherNationalID { get; set; }

        public DateTime? FatherNationalDNoIssueDate { get; set; }

        public DateTime? FatherNationalDNoExpiryDate { get; set; }

        [StringLength(100)]
        public string FatherMobileNumberTwo { get; set; }

        [StringLength(100)]
        public string FatherCompanyName { get; set; }

        public int? CanYouVolunteerToHelpOneID { get; set; }

        [StringLength(100)]
        public string MotherFirstName { get; set; }

        [StringLength(100)]
        public string MotherMiddleName { get; set; }

        [StringLength(100)]
        public string MotherLastName { get; set; }

        public int? MotherCountryID { get; set; }

        [StringLength(100)]
        public string MotherPassportNumber { get; set; }

        public DateTime? MotherPassportNoIssueDate { get; set; }

        public DateTime? MotherPassportNoExpiryDate { get; set; }

        public int? MotherPassportCountryofIssueID { get; set; }

        [StringLength(100)]
        public string MotherNationalID { get; set; }

        public DateTime? MotherNationalDNoIssueDate { get; set; }

        public DateTime? MotherNationalDNoExpiryDate { get; set; }

        public byte? MotherStudentRelationShipID { get; set; }

        [StringLength(100)]
        public string MotherEmailID { get; set; }

        [StringLength(100)]
        public string MotherCompanyName { get; set; }

        public int? CanYouVolunteerToHelpTwoID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(10)]
        public string ParentCode { get; set; }

        [StringLength(100)]
        public string GuardianFirstName { get; set; }

        [StringLength(100)]
        public string GuardianMiddleName { get; set; }

        [StringLength(100)]
        public string GuardianLastName { get; set; }

        [StringLength(100)]
        public string GuardianCompanyName { get; set; }

        [StringLength(50)]
        public string FatherEmailID { get; set; }

        public int? GuardianNationalityID { get; set; }

        [StringLength(50)]
        public string GuardianNationalID { get; set; }

        public DateTime? GuardianNationalIDNoIssueDate { get; set; }

        public DateTime? GuardianNationalIDNoExpiryDate { get; set; }

        [StringLength(50)]
        public string GuardianPassportNumber { get; set; }

        public int? GuardianCountryofIssueID { get; set; }

        public DateTime? GuardianPassportNoIssueDate { get; set; }

        public DateTime? GuardianPassportNoExpiryDate { get; set; }

        [StringLength(30)]
        public string GuardianWhatsappMobileNo { get; set; }

        [StringLength(30)]
        public string FatherWhatsappMobileNo { get; set; }

        [StringLength(30)]
        public string MotherWhatsappMobileNo { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Login Login { get; set; }
        
        public virtual School School { get; set; }

        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }

        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }

        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        
        public virtual ICollection<Student> Students { get; set; }

    }
}