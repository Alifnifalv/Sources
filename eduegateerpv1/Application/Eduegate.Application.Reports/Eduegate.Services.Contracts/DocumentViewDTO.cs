using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class DocumentViewDTO
    {
        [DataMember]
        public List<DocumentFileDTO> Documents { get; set; }
        //[DataMember]
        //public string ReferenceParameterName { get { return "referenceID"; } }
        //[DataMember]
        //public string EntityParameterName { get { return "entityType"; } }
    }
}
