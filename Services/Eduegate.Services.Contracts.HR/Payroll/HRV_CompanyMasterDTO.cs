using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class HRV_CompanyMasterDTO
    {
        [DataMember]
        public short COUNTRY_ID { get; set; }

        [DataMember]
        public Nullable<int> COMPANY_CODE { get; set; }
        [DataMember]
        public string COMPANY_NAME { get; set; }
        [DataMember]
        public string COMPANY_SHORT_NAME { get; set; }
    }
}
