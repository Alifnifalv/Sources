using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Logging
{
    [DataContract]
    public class CatalogLoggerDTO
    {
        [DataMember]
        public long ProductSKUMapID { get; set; }
        [DataMember]
        public int OperationTypeID { get; set; }
        [DataMember]
        public string LogValue { get; set; }
        [DataMember]
        public string SolrCore { get; set; }
    }
}
