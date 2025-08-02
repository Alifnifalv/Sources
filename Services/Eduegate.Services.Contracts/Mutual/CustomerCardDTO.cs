using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class CustomerCardDTO
    {
        [DataMember]
        public long? LoginID;
        [DataMember]
        public long? CustomerID;
        [DataMember]
        public int? CardTypeID;
        [DataMember]
        public string CardNumber;
    }
}
