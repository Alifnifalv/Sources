using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class SearchCatalogGroupDTO
    {
        [DataMember]
        public List<SearchCatalogDTO> Catalogs { get; set; }
        [DataMember]
        public string GroupValue { get; set; }
        [DataMember]
        public int CatalogCount { get; set; }
        [DataMember]
        public SearchCatalogDTO SelectedCatalog { get; set; }
    }
}
