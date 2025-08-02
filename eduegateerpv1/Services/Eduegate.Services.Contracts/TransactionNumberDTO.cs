using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class TransactionNumberDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public int Month { get; set; }
        [DataMember]
        public string PaymentMode { get; set; }
    }
}
