using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ReportDTO
    {
        [DataMember]
        public List<KeyValueParameterDTO> SystemParameters { get; set; }
        [DataMember]
        public List<KeyValueParameterDTO> ReportParameters { get; set; }
    }
}
