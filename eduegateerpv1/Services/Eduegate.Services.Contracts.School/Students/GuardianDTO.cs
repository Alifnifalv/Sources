using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    public class GuardianDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ParentStudentMapIID { get; set; }

        [DataMember]
        public long ParentIID { get; set; }
        [DataMember]
        public string FatherName { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string FatherOccupation { get; set; }
        [DataMember]
        public string FatherProfile { get; set; }
        [DataMember]
        public string MotherName { get; set; }
        [DataMember]
        public string MotherPhone { get; set; }
        [DataMember]
        public string MotherOccupation { get; set; }
        [DataMember]
        public string MotherPofile { get; set; }
        [DataMember]
        public byte? GuardianTypeID { get; set; }
        [DataMember]
        public string GuardianName { get; set; }
        [DataMember]
        public string GuardianRelation { get; set; }
        [DataMember]
        public string GaurdianEmail { get; set; }
        [DataMember]
        public string GuardianPhoto { get; set; }
        [DataMember]
        public string GuardianPhone { get; set; }
        [DataMember]
        public string GuardianOccupation { get; set; }
        [DataMember]
        public string GuardianAddress { get; set; }
        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public string BuildingNo { get; set; }

        [DataMember]
        public string FlatNo { get; set; }

        [DataMember]
        public string StreetNo { get; set; }

        [DataMember]
        public string StreetName { get; set; }

        [DataMember]
        public string LocationNo { get; set; }

        [DataMember]
        public string LocationName { get; set; }

        [DataMember]
        public string ZipNo { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PostBoxNo { get; set; }

        [DataMember]
        public int? CountryID { get; set; }
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string FatherFirstName { get; set; }

        [DataMember]
        public string FatherMiddleName { get; set; }

        [DataMember]
        public string FatherLastName { get; set; }

        [DataMember]
        public int? FatherCountryID { get; set; }

        [DataMember]
        public string FatherCountry { get; set; }

        [DataMember]
        public string GuardianNationality { get; set; }

        [DataMember]
        public string FatherPassportNumber { get; set; }

        [DataMember]
        public DateTime? FatherPassportNoIssueDate { get; set; }

        [DataMember]
        public DateTime? FatherPassportNoExpiryDate { get; set; }

        [DataMember]
        public int? FatherPassportCountryofIssueID { get; set; }
        
        [DataMember]
        public string FatherPassportCountryofIssue { get; set; }
        
        [DataMember]
        public string FatherNationalID { get; set; }

        [DataMember]
        public DateTime? FatherNationalDNoIssueDate { get; set; }

        [DataMember]
        public DateTime? FatherNationalDNoExpiryDate { get; set; }

        [DataMember]
        public string FatherMobileNumberTwo { get; set; }

        [DataMember]
        public string FatherCompanyName { get; set; }

        [DataMember]
        public int? CanYouVolunteerToHelpOneID { get; set; }

        [DataMember]
        public string CanYouVolunteerToHelpOne { get; set; }
        [DataMember]
        public string MotherFirstName { get; set; }

        [DataMember]
        public string MotherMiddleName { get; set; }

        [DataMember]
        public string MotherLastName { get; set; }

        [DataMember]
        public int? MotherCountryID { get; set; }

        [DataMember]
        public string MotherCountry { get; set; }

        [DataMember]
        public string MotherPassportNumber { get; set; }

        [DataMember]
        public DateTime? MotherPassportNoIssueDate { get; set; }

        [DataMember]
        public DateTime? MotherPassportNoExpiryDate { get; set; }

        [DataMember]
        public int? MotherPassportCountryofIssueID { get; set; }

        [DataMember]
        public string MotherPassportCountryofIssue { get; set; }

        [DataMember]
        public string GuardianCountryofIssue { get; set; }

        [DataMember]
        public string MotherNationalID { get; set; }

        [DataMember]
        public DateTime? MotherNationalDNoIssueDate { get; set; }

        [DataMember]
        public DateTime? MotherNationalDNoExpiryDate { get; set; }

        [DataMember]
        public byte? MotherStudentRelationShipID { get; set; }

        [DataMember]
        public string MotherStudentRelationShip { get; set; }

        [DataMember]
        public string MotherEmailID { get; set; }

        [DataMember]
        public string MotherCompanyName { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public int? CanYouVolunteerToHelpTwoID { get; set; }

        [DataMember]
        public string CanYouVolunteerToHelpTwo { get; set; }

        [DataMember]
        public string GuardianFirstName { get; set; }

        [DataMember]
        public string GuardianMiddleName { get; set; }

        [DataMember]
        public string GuardianLastName { get; set; }

        [DataMember]
        public string GuardianCompanyName { get; set; }

        [DataMember]
        public string FatherEmailID { get; set; }

        [DataMember]
        public int? GuardianNationalityID { get; set; }

        [DataMember]
        public string GuardianNationalID { get; set; }

        [DataMember]
        public DateTime? GuardianNationalIDNoIssueDate { get; set; }

        [DataMember]
        public DateTime? GuardianNationalIDNoExpiryDate { get; set; }

        [DataMember]
        public string GuardianPassportNumber { get; set; }

        [DataMember]
        public int? GuardianCountryofIssueID { get; set; }

        [DataMember]
        public DateTime? GuardianPassportNoIssueDate { get; set; }

        [DataMember]
        public DateTime? GuardianPassportNoExpiryDate { get; set; }

        [DataMember]
        public string GuardianWhatsappMobileNo { get; set; }

        [DataMember]
        public string FatherWhatsappMobileNo { get; set; }

        [DataMember]
        public string MotherWhatsappMobileNo { get; set; }
        public object LatestMessage { get; set; }

        [DataMember]
        public string LastMessageText { get; set; }

        [DataMember]
        public DateTime? LastMessageDate { get; set; }
         
    }
}
