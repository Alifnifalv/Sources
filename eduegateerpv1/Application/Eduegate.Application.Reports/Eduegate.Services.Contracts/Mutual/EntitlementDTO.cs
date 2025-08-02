using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public partial class EntitlementDTO
    {
        [DataMember]
        public List<EntitlementMapDTO> EntitlementMaps { get; set; }
    }
}
