using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class KeywordsDTO
    {
        [DataMember]
        public long LogID { get; set; }
        [DataMember]
        public string Keyword { get; set; }
    }
}
