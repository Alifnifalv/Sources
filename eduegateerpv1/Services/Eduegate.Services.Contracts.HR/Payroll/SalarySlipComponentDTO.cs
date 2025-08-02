using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class SalarySlipComponentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SalarySlipComponentDTO()
        {
            SalaryComponent = new KeyValueDTO();
        }

        [DataMember]
        public long SalarySlipIID { get; set; }

        [DataMember]
        public int? SalaryComponentID { get; set; }

        [DataMember]
        public byte? SalaryComponentTypeID { get; set; }

        [DataMember]
        public KeyValueDTO SalaryComponent { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal? NoOfDays { get; set; }

        [DataMember]
        public decimal? NoOfHours { get; set; }

        [DataMember]
        public bool? IsVerified { get; set; }

        [DataMember]
        public long? ReportContentID { get; set; }

        [DataMember]
        public decimal? Earnings { get; set; }

        [DataMember]
        public decimal? Deduction { get; set; }
    }
}