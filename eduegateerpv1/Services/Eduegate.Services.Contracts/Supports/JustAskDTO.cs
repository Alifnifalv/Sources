using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Supports
{
    [DataContract]
   public class JustAskDTO
    {
        [DataMember]
        public long JustAskIID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public byte CultureID { get; set; }
        [DataMember]
        public long? CreatedBy { get; set; }
    }
}
