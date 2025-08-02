using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class SearchParameterDTO
    {
        public const int DefaultPageSize = 30;

        public SearchParameterDTO()
        {
            Parameters = new Dictionary<string, string>();
            ExtraAndParams = new Dictionary<string, string>();
            ExtraOrParams = new Dictionary<string, string>();
            Facets = new Dictionary<string, string>();
            RangeParameters = new Dictionary<string, string>();
            PageSize = DefaultPageSize;
            PageIndex = 1;
        }

        [DataMember]
        public string FreeSearch { get; set; }

        [DataMember]
        public string FreeSearchExtraKey { get; set; }

        [DataMember]
        public string FreeSearchExtraValue { get; set; }

        [DataMember]
        public string FreeSearchFilterKey { get; set; }

        [DataMember]
        public string FreeSearchFilterValue { get; set; }

        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public IDictionary<string, string> Parameters { get; set; }
        [DataMember]
        public IDictionary<string, string> RangeParameters { get; set; }
        [DataMember]
        public IDictionary<string, string> Facets { get; set; }
        [DataMember]
        public IDictionary<string, string> ExtraAndParams { get; set; }
        [DataMember]
        public IDictionary<string, string> ExtraOrParams { get; set; }
        [DataMember]
        public string Sort { get; set; }

        [DataMember]
        public int FirstItemIndex
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
            set { }

        }

        [DataMember]
        public int LastItemIndex
        {
            get
            {
                return FirstItemIndex + PageSize;
            }
            set { }

        }
        [DataMember]
        public decimal ConversionRate { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public int CountryID { get; set; }


        [DataMember]
        public long CustomerID { get; set; }

        [DataMember]
        public int SiteID { get; set; }

        [DataMember]
        public decimal SliderMaxPrice {get;set;}
    }
}
