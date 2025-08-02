using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
   public class FeeMasterAccountSettingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO 
    {
        [DataMember]
        public long? LedgerAccountID { get; set; }
        [DataMember]
        public long? TaxLedgerAccountID { get; set; }
        [DataMember]
        public decimal? TaxPercentage { get; set; }

    }
}
