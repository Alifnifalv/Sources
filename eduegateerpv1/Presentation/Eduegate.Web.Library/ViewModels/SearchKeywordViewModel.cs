using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Web.Library.ViewModels
{
    public class SearchKeywordViewModel
    {
        public string Keywords { get; set; }

        public static SearchKeywordViewModel ToVM(SearchKeywordsDictionaryDTO dto)
        {
            Mapper<SearchKeywordsDictionaryDTO, SearchKeywordViewModel>.CreateMap();
            return Mapper<SearchKeywordsDictionaryDTO, SearchKeywordViewModel>.Map(dto);
        }
    }
}
