using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Logging
{
    [DataContract]
    public class DataHistoryResultDTO
    {
        [DataMember]
        public int DataHistoryEntityID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<DataHistoryDTO> HistoryData { get; set; }
    }
}
