using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class PayrollFrequenciesDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte PayrollFrequencyID { get; set; }

        [DataMember]
        public string FrequencyName { get; set; }
    }
}
