using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductSKUDetailsDTO
    {
        [DataMember]
        public string SerialNo { get; set; }

        [DataMember]
        public long DetailID { get; set; }

    }
}
