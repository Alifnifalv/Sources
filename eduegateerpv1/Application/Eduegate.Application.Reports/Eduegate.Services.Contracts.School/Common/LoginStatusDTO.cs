using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Common
{
    [DataContract]
    public class LoginStatusDTO
    {
        [DataMember]
        public decimal StatusID { get; set; }

        [DataMember]
        public string StatusDescription { get; set; }
    }
}
