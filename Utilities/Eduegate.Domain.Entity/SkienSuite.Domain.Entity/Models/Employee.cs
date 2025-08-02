using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Employee
    {
        public Employee()
        {
            this.DocumentFiles = new List<DocumentFile>();
            this.InventoryVerifications = new List<InventoryVerification>();
            this.TransactionHeads = new List<TransactionHead>();
            this.JobEntryHeads = new List<JobEntryHead>();
            this.EmployeeRoleMaps = new List<EmployeeRoleMap>();
            this.Employees1 = new List<Employee>();
            this.ServiceEmployeeMaps = new List<ServiceEmployeeMap>();
        }

        public long EmployeeIID { get; set; }
        public string EmployeeAlias { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<int> EmployeeRoleID { get; set; }
        public Nullable<int> DesignationID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public string EmployeePhoto { get; set; }
        public string WorkEmail { get; set; }
        public string WorkPhone { get; set; }
        public string WorkMobileNo { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> JobTypeID { get; set; }
        public Nullable<int> GenderID { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public Nullable<int> MaritalStatusID { get; set; }
        public Nullable<long> ReportingEmployeeID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string PersonalMobileNo { get; set; }
        public string CivilIDNumber { get; set; }
        public Nullable<System.DateTime> CivilIDValidity { get; set; }
        public string SponsorDetails { get; set; }
        public Nullable<int> SalaryMethodID { get; set; }
        public Nullable<long> EmployeeBankID { get; set; }
        public Nullable<int> AliasID { get; set; }
        public Nullable<int> PassportStatus { get; set; }
        public string Address { get; set; }
        public Nullable<int> ResidencyCompanyId { get; set; }
        public virtual Login Login { get; set; }
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual EmployeeBankDetail EmployeeBankDetail { get; set; }
        public virtual ICollection<EmployeeRoleMap> EmployeeRoleMaps { get; set; }
        public virtual EmployeeRole EmployeeRole { get; set; }
        public virtual ICollection<Employee> Employees1 { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual SalaryMethod SalaryMethod { get; set; }
        public virtual ICollection<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }
    }
}
