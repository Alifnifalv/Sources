using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Leaves;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class EmployeeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeDTO()
        {
            PayrollInfo = new EmployeePayrollDTO();
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
        public List<ContactDTO> Contacts { get; set; }
        
        [DataMember]
        public LoginDTO Login { get; set; }
        
        [DataMember]
        public Nullable<int> MaritalStatusID { get; set; }
        
        [DataMember]
        public Nullable<long> ReportingEmployeeID { get; set; }
        
        [DataMember]
        public EmployeeAdditionalInfoDTO AdditionalInfo { get; set; }

        [DataMember]
        public PassportVisaDetailDTO PassportVisaInfo { get; set; }

        [DataMember]
        public EmployeePayrollDTO PayrollInfo { get; set; }

        [DataMember]
        public EmployeeAirFareDTO AirFareInfo { get; set; }

        [DataMember]
        public EmployeeBankDetailDTO BankDetailInfo { get; set; }

        [DataMember]
        public List<EmployeeLeaveAllocationDTO> EmployeeLeaveAllocationInfo { get; set; }

        [DataMember]
        public List<EmployeeAcademicQualificationDTO> AcademicDetails { get; set; }

        [DataMember]
        public List<EmployeeExperienceDTO> ExperienceDetails { get; set; }

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
        public DateTime? ResignationDate { get; set; }

        [DataMember]
        public int? Grade { get; set; }

        [DataMember]
        public int? PassageTypeID { get; set; }

        [DataMember]
        public int? AccomodationTypeID { get; set; }

        [DataMember]
        public int? LeaveGroupID { get; set; }

        [DataMember]
        public bool? IsOverrideLeaveGroup { get; set; }

        [DataMember]
        public string CBSEID { get; set; }

        [DataMember]
        public DateTime? InActiveDate { get; set; }

        [DataMember]
        public string FromDateString { get; set; }

        [DataMember]
        public string ToDateString { get; set; }

        [DataMember]
        public List<EmployeeRelationsDetailDTO> RelationDetails { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }
        
        [DataMember]
        public string DesignationName { get; set; } 
        
        [DataMember]
        public string NationalIDNo { get; set; } 

        [DataMember]
        public byte? StatusID { get; set; }

        public object LatestMessage { get; set; }

        [DataMember]
        public string LastMessageText { get; set; }

        [DataMember]
        public DateTime? LastMessageDate { get; set; }

        [DataMember]
        public long CommentIID { get; set; }   
        
        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public long? UnreadCommentsCount { get; set; }

        [DataMember]
        public long? JobInterviewMapID { get; set; } 
    }
}