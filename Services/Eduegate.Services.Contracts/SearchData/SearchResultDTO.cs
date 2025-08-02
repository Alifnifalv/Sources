using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class SearchResultDTO
    {
        [DataMember]
        public List<SearchCatalogDTO> Catalogs { get; set; }
        [DataMember]
        public List<FacetsDetail> FacetsDetails { get; set; }
        [DataMember]
        public Int32 TotalProductsCount { get; set; }
        [DataMember]
        public decimal SliderMaxPrice { get; set; }
        [DataMember]
        public string SearchedText { get; set; }
        [DataMember]
        public string DefaultSort { get; set; }
        [DataMember]
        public List<SearchCatalogGroupDTO> CatalogGroups { get; set; }
    }
}
