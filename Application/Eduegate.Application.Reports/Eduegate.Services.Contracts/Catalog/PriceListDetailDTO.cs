using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class PriceListDetailDTO
    {
        [DataMember]
        public long PriceListID { get; set; }
        [DataMember]
        public string PriceDescription { get; set; }
    }
}
