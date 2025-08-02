using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class BlinkKeywordCatalogDTO
    {
        [DataMember]
        public string ProductName { get; set; }
    }
}
