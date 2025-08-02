using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "BankReconciliationStatuses")]
    public enum BankReconciliationStatuses
    {
        [EnumMember]
        Draft =1,
        [EnumMember]
        Posted = 2,
        
    }
}
