using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class SalarySlipEmployeeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SalarySlipEmployeeDTO()
        {

        }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public long SalarySlipIID { get; set; }

        [DataMember]
        public string SlipDate { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public bool IsRegenerate { get; set; } = false;

        [DataMember]
        public bool IsSelected { get; set; } = false;

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
        public int? SlipPublishStatusID { get; set; }

        [DataMember]
        public string SlipPublishStatusName { get; set; }

        [DataMember]
        public bool? IsVerified { get; set; }

        [DataMember]
        public decimal? EarningAmount { get; set; }

        [DataMember]
        public decimal? DeductingAmount { get; set; }

        [DataMember]
        public decimal? AmountToPay { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

    }
}