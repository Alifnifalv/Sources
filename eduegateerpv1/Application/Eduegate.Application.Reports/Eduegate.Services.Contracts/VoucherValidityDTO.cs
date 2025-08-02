using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class VoucherValidityDTO
    {
        [DataMember]
        public string VoucherNumber { get; set; }
        [DataMember]
        public string DateUsed { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public String Amount { get; set; }
        [DataMember]
        public string Info { get; set; }
    }
}
