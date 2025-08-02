using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class CustomerStatusDTO
    {
        [DataMember]
        public decimal StatusID { get; set; }

        [DataMember]
        public string StatusName { get; set; }
    }
}
