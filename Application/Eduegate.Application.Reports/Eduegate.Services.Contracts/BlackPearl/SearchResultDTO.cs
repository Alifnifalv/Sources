using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class SearchResultDTO
    {
        [DataMember]
        public long ProductIID { get; set; }
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public Nullable<decimal> Amount { get; set; }
        [DataMember]
        public Nullable<byte> SortOrder { get; set; }
        [DataMember]
        public string SKU { get; set; }
        [DataMember]
        public string ImageFile { get; set; }
        [DataMember]
        public Nullable<byte> Sequence { get; set; }
    }


    public static class SearchResultMapper
    {
        public static SearchResultDTO ConvertToDTO(this SearchResult searchResult)
        {
            return new SearchResultDTO
            {
                ProductIID = searchResult.ProductIID,
                ProductCode = searchResult.ProductCode,
                ProductName = searchResult.ProductName,
                Amount = searchResult.Amount,
                SortOrder = searchResult.SortOrder,
                SKU = searchResult.SKU,
                ImageFile = searchResult.ImageFile,
                Sequence = searchResult.Sequence
            };
        }

        public static IEnumerable<SearchResultDTO> ConvertToDTO(this IEnumerable<SearchResult> searchResult)
        {
            return searchResult.Select(x => x.ConvertToDTO());
        }

        public static SearchResultDTO ToSearchResultDTOMap(SearchResult searchResult)
        {
            return new SearchResultDTO()
            {
                ProductIID = searchResult.ProductIID,
                ProductCode = searchResult.ProductCode,
                ProductName = searchResult.ProductName,
                Amount = searchResult.Amount,
                SortOrder = searchResult.SortOrder,
                SKU = searchResult.SKU,
                ImageFile = searchResult.ImageFile,
                Sequence = searchResult.Sequence
            };
        }
    }

}
