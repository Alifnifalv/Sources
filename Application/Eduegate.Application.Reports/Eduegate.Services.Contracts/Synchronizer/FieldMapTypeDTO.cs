using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums.Synchronizer;

namespace Eduegate.Services.Contracts.Synchronizer
{
    [DataContract]
    public class FieldMapTypeDTO
    {
        [DataMember]
        public int FieldMapID { get; set; }
        [DataMember]
        public FieldMapTypes FieldMapType { get; set; }
        [DataMember]
        public string SourceField { get; set; }
        [DataMember]
        public string DestinationField { get; set; }
    }
}
