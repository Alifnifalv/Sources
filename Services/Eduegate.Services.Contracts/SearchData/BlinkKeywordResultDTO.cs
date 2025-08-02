using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class BlinkKeywordResultDTO
    {
        [DataMember]
        public List<BlinkKeywordCatalogDTO> ProductNames { get; set; }
    }
}
