using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Domain.Mappers
{
    //public class KeywordDictionaryMapper : IDTOEntityMapper<SearchKeywordsDictionaryDTO, KeywordsDictionary>
    //{
    //    private CallContext _context;

    //    public static KeywordDictionaryMapper Mapper(CallContext context)
    //    {
    //        var mapper = new KeywordDictionaryMapper();
    //        mapper._context = context;
    //        return mapper;
    //    }

    //    public SearchKeywordsDictionaryDTO ToDTO(KeywordsDictionary entity)
    //    {
    //        if (entity != null)
    //        {
    //            return new SearchKeywordsDictionaryDTO()
    //            {
    //                Keywords = entity.Keywords
    //            };
    //        }
    //        else
    //        {
    //            return new SearchKeywordsDictionaryDTO();
    //        }
    //    }

    //    public KeywordsDictionary ToEntity(SearchKeywordsDictionaryDTO dto)
    //    {
    //        throw new NotImplementedException();
    //    }

    //}
}
