using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Search
{
    [DataContract]
    public class ViewActionDTO
    {
        [DataMember]
        public long ViewActionID { get; set; }
        [DataMember]
        public Nullable<long> ViewID { get; set; }
        [DataMember]
        public string ActionCaption { get; set; }
        [DataMember]
        public string ActionAttribute { get; set; }
        [DataMember]
        public string ActionAttribute2 { get; set; }
    }
}
