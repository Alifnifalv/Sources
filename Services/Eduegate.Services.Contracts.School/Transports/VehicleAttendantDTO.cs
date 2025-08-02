using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class VehicleAttendantDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeeIID { get; set; }

        [DataMember]
        public string EmployeeAlias { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string EmployeeFullName { get; set; }

        [DataMember]
        public string EmployeeFullNamewithCode { get; set; }

        [DataMember]
        public string EmployeePhoto { get; set; }

        [DataMember]
        public string WorkMobileNo { get; set; }

        [DataMember]
        public string WorkEmail { get; set; }

        [DataMember]
        public string WorkPhone { get; set; }

        [DataMember]
        public DateTime? DateOfJoining { get; set; }

        [DataMember]
        public DateTime? DateOfBirth { get; set; }

        [DataMember]
        public int? Age { get; set; }

        [DataMember]
        public string AdhaarCardNo { get; set; }

        [DataMember]
        public int? DesignationID { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public int? JobTypeID { get; set; }

        [DataMember]
        public byte? GenderID { get; set; }

        [DataMember]
        public long? DepartmentID { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public int? MaritalStatusID { get; set; }

        [DataMember]
        public long? ReportingEmployeeID { get; set; }

        [DataMember]
        public string ReportingEmployeeName { get; set; }

        [DataMember]
        public byte? CastID { get; set; }

        [DataMember]
        public byte? RelegionID { get; set; }

        [DataMember]
        public string PermenentAddress { get; set; }

        [DataMember]
        public string PresentAddress { get; set; }

        [DataMember]
        public byte? CommunityID { get; set; }

        [DataMember]
        public int? NationalityID { get; set; }

        [DataMember]
        public string DateOfBirthString { get; set; }

        [DataMember]
        public string JoiningDateString { get; set; }

        [DataMember]
        public string GenderName { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public string DesignationName { get; set; }
    }
}