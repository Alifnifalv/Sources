using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public class GeoLocationDTO
    {
        [DataMember]
        public string lat { get; set; }
        [DataMember]
        public string lon { get; set; }
    }
}
