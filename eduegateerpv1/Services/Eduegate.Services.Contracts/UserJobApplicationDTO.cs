using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class UserJobApplicationDTO
    {
        [DataMember]
        public long JobApplicationIID { get; set; }
        [DataMember]
        public long JobID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Resume { get; set; }
        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public byte CultureID { get; set; }
    }
}
