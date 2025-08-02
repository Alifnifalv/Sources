using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class SalarySlipDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SalarySlipDTO()
        {
            Department = new List<KeyValueDTO>();
            Employees = new List<KeyValueDTO>();
            SalaryComponent = new List<SalarySlipComponentDTO>();
        }

        [DataMember]
        public long SalarySlipIID { get; set; }

        [DataMember]
        public DateTime? SlipDate { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public int? SalaryComponentID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public byte? SalarySlipStatusID { get; set; }

        [DataMember]
        public string SalarySlipStatusName { get; set; }

        [DataMember]
        public List<KeyValueDTO> Department { get; set; }

        [DataMember]
        public List<KeyValueDTO> Employees { get; set; }

        [DataMember]
        public KeyValueDTO SalaryComponentKeyValue { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string EmployeeWorkEmail { get; set; }

        [DataMember]
        public bool IsRegenerate { get; set; } = false;

        [DataMember]
        public bool? IsGenerateLeaveOrVacationsalary { get; set; } 

        [DataMember]
        public decimal? NoOfDays { get; set; }

        [DataMember]
        public decimal? NoOfHours { get; set; }

        [DataMember]
        public bool? IsVerified { get; set; }

        [DataMember]
        public long? ReportContentID { get; set; }

        [DataMember]
        public decimal? WorkingDays { get; set; }

        [DataMember]
        public decimal? NormalHours { get; set; }

        [DataMember]
        public decimal? OTHours { get; set; }

        [DataMember]
        public decimal? LOPDays { get; set; }

        [DataMember]
        public decimal? EarningAmount { get; set; }

        [DataMember]
        public decimal? DeductingAmount { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public byte[] ReportData { get; set; }

        [DataMember]
        public string ReportName { get; set; }

        [DataMember]
        public string SlipDateString { get; set; }

        [DataMember]
        public List<SalarySlipComponentDTO> SalaryComponent { get; set; }

    }
}