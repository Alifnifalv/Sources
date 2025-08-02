using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.Settings;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class UserDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long UserID { get; set; }

        [DataMember]
        public string LoginID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string ProfileFile { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string LoginUserID { get; set; }

        [DataMember]
        public string LoginEmailID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public string PasswordSalt { get; set; }

        [DataMember]
        public bool IsRequired { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string PasswordHint { get; set; }
        
        [DataMember]
        public string LastOTP { get; set; }

        [DataMember]
        public byte? StatusID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string School { get; set; }

        [DataMember]
        public string LastLoginDate { get; set; }

        [DataMember]
        public Nullable<int> SiteID { get; set; }

        [DataMember]
        public Nullable<int> RegisteredCountryID { get; set; }

        [DataMember]
        public string RegisteredIP { get; set; }

        [DataMember]
        public string RegisteredIPCountry { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string AddressLongitude { get; set; }

        [DataMember]
        public string AddressLatitude { get; set; }

        [DataMember]
        public List<UserRoleDTO> Roles { get; set; }

        [DataMember]
        public List<string> UserClaims { get; set; }

        [DataMember]
        public List<ClaimDetailDTO> Claims { get; set; }

        [DataMember]
        public List<KeyValueDTO> ClaimSets { get; set; }

        [DataMember]
        public BranchDTO Branch { get; set; }

        [DataMember]
        public CustomerDTO Customer { get; set; }

        [DataMember]
        public SupplierDTO Supplier { get; set; }

        [DataMember]
        public EmployeeDTO Employee { get; set; }

        [DataMember]
        public List<ContactDTO> Contacts { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public GeoLocationDTO Geolocation { get; set; }

        [DataMember]
        public List<SettingDTO> UserSettings { get; set; }

        [DataMember]
        public bool IsProfileCompleted { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public bool? IsDriver { get; set; }

        [DataMember]
        public string QID { get; set; }

        [DataMember]
        public string PassportNumber { get; set; }

        [DataMember]
        public GuardianDTO Parent { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentFullName { get; set; }
    }
}