using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "BankReconciliationMatchedStatuses")]
    public enum BankReconciliationMatchedStatuses
    {
       
        [EnumMember]
        Matched = 1,
        [EnumMember]
        MisMatchWithLedger = 2,
        [EnumMember]
        MisMatchWithBank = 3,
        
    }
}
