using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.HR.Employee
{
    [DataContract]
    public class EmployeesDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeesDTO()
        {
            EmployeeRoles = new List<KeyValueDTO>();
            Contacts = new List<ContactsDTO>();
            AdditionalInfo = new EmployeesAdditionalInfoDTO();
        }

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
        public List<ContactsDTO> Contacts { get; set; }

        [DataMember]
        public Nullable<int> MaritalStatusID { get; set; }

        [DataMember]
        public Nullable<long> ReportingEmployeeID { get; set; }

        [DataMember]
        public EmployeesAdditionalInfoDTO AdditionalInfo { get; set; }

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
        public string DateOfBirthString { get; set; }

        [DataMember]
        public string JoiningDateString { get; set; }

        [DataMember]
        public string GenderName { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public string DesignationName { get; set; }

        [DataMember]
        public string Vehicle { get; set; }

        [DataMember]
        public string RouteCode { get; set; }

    }
}