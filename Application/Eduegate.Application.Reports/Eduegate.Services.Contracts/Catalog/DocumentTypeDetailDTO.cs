using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class DocumentTypeDetailDTO
    {
        [DataMember]
        public long DocumentTypeID { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
    }
}
