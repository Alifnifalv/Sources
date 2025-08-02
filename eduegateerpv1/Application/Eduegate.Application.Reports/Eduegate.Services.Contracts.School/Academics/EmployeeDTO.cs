using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class EmployeeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeeIID { get; set; }

        [DataMember]
        public string EmployeeAlias { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public Nullable<int> DesignationID { get; set; }

        [DataMember]
        public Nullable<long> BranchID { get; set; }

        [DataMember]
        public string EmployeePhoto { get; set; }

        [DataMember]
        public string WorkMobileNo { get; set; }

        [DataMember]
        public string WorkEmail { get; set; }

        [DataMember]
        public string WorkPhone { get; set; }

        [DataMember]
        public Nullable<System.DateTime> DateOfJoining { get; set; }

        [DataMember]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [DataMember]
        //public Nullable<int> Age { get; set; }
        public int? Age { get; set; }

        [DataMember]
        public Nullable<int> JobTypeID { get; set; }

        [DataMember]
        public Nullable<byte> GenderID { get; set; }

        [DataMember]
        public Nullable<long> DepartmentID { get; set; }

        [DataMember]
        public Nullable<int> CompanyID { get; set; }

        [DataMember]
        public List<KeyValueDTO> EmployeeRoles { get; set; }

        [DataMember]
        public Nullable<int> MaritalStatusID { get; set; }

        [DataMember]
        public Nullable<long> ReportingEmployeeID { get; set; }


        [DataMember]
        public string ReportingEmployeeName { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

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
        public string AdhaarCardNo { get; set; }

        [DataMember]
        public int? NationalityID { get; set; }

        [DataMember]
        public byte? LicenseTypeID { get; set; }

        [DataMember]
        public string LicenseNumber { get; set; }

        [DataMember]
        public string LicenseType { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public string NationalityName { get; set; }

        [DataMember]
        public int? BloodGroupID { get; set; }

        [DataMember]
        public string EmergencyContactNo { get; set; }

        [DataMember]
        public long? SignatureContentID { get; set; }

        [DataMember]
        public DateTime? LastWorkingDate { get; set; }

        [DataMember]
        public byte? LeavingTypeID { get; set; }

        [DataMember]
        public DateTime? ConfirmationDate { get; set; }

        [DataMember]
        public string Grades { get; set; }


        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string Designation { get; set; }

        [DataMember]
        public string ProfileImage { get; set; }

        [DataMember]
        public string DateOfBirthString { get; set; }

        [DataMember]
        public string PresentStatus { get; set; }

        //ForClassTeacher -- DashBoarduse

        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public string Section { get; set; }

        [DataMember]
        public string TotalStudentStrength { get; set; }

        [DataMember]
        public string NoOfStudentsPresent { get; set; }

        [DataMember]
        public string NoOfStudentsAbsent { get; set; }
    }
}
