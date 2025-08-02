using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class NewsLetterDTO
    {
        [DataMember]
        public bool Subscribe { get; set; }

        [DataMember]
        public bool UnSubscribe { get; set; }
    }
}
