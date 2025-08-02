using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class SearchKeywordsDictionaryDTO
    {
        [DataMember]
        public string Keywords { get; set; }
    }
}
