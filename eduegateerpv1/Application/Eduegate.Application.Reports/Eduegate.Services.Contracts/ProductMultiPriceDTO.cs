using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class ProductMultiPriceDTO
    {
        [DataMember]
        public string GroupID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string MultipriceValue { get; set; }
        [DataMember]
        public bool isSelected { get; set; }

        [DataMember]
        public string Currency { get; set; }
    }
}
