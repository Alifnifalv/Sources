using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class SalaryMethodDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int SalaryMethodID { get; set; }

        [DataMember]
        public string SalaryMethodName { get; set; }

    }
}
