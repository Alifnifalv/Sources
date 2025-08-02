using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class PhoneDTO
    {
        [DataMember]
        public long EntityPropertyID { get; set; }

        [DataMember]
        public string EntityProperty { get; set; } 

        [DataMember]
        public int EntityTypeID { get; set; }

        [DataMember]
        public long EntityPropertyTypeID { get; set; }

        [DataMember]
        public string TelephoneCode { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }

        [DataMember]
        public long ReferenceID { get; set; }

        [DataMember]
        public int Sequence { get; set; }
    }
}
