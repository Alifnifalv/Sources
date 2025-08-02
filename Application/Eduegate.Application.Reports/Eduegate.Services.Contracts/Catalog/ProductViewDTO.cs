using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductViewDTO
    {
        [DataMember]
        public long TotalProduct { get; set; }

        [DataMember]
        public string RecentlyAdded { get; set; }

        [DataMember]
        public string MostSellingProduct { get; set; }

        [DataMember]
        public long OutOfStocks { get; set; }

        [DataMember]
        public long PendingCreate { get; set; }

        [DataMember]
        public List<ProductItemDTO> ProductItems { get; set; }
    }
}
