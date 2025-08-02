using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class NewsletterDTO
    {
        [DataMember]
        public short result { get; set; }
        [DataMember]
        public string message { get; set; }
    }
}
