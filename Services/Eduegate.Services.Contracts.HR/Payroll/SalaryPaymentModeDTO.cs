using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class SalaryPaymentModeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int SalaryPaymentModeID { get; set; }

        [DataMember]
        public string PaymentName { get; set; }

        [DataMember]
        public byte? PyamentModeTypeID { get; set; }

    }
}
