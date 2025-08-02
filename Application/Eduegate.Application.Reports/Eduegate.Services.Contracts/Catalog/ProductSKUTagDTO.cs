using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ProductSKUTagDTO
    {
        [DataMember]
        public List<long> SelectedTagIds { get; set; }
        [DataMember]
        public List<string> SelectedTagNames { get; set; }
        [DataMember]
        public List<long> IDs { get; set; }
    }
}
