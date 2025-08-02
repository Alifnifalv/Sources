using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class BankAccountDTO
    {
        [DataMember]
        public string AccountHolderName { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string IBAN { get; set; }

        [DataMember]
        public string SwiftCode { get; set; }

        [DataMember]
        public string BankShortName { get; set; }
    }
}
