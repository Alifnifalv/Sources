using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Salon
{
    [DataContract]
    public class ServicePriceDTO
    {
        [DataMember]
        public KeyValueDTO Duration { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal DiscountPrice { get; set; }
    }
}
