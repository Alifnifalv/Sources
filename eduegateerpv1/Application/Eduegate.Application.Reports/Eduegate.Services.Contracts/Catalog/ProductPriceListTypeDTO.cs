using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductPriceListTypeDTO
    {
        [DataMember]
        public short ProductPriceListTypeID { get; set; }
        [DataMember]
        public string PriceListTypeName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
