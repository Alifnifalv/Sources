using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class DataFeedTypeDTO
    {
        [DataMember]
        public int DataFeedTypeID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string TemplateName { get; set; }
    }
}
