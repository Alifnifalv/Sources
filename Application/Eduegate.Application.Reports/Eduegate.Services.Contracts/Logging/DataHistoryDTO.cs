using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Logging
{
    [DataContract]
    public class DataHistoryDTO
    {
        [DataMember]
        public long LogIID { get; set; }
        [DataMember]
        public long RecordIID { get; set; }
        [DataMember]
        public DateTime UpdatedDate { get; set; }
        [DataMember]
        public int UserIID { get; set; }
        [DataMember]
        public string UpdatedUserName { get; set; }
        [DataMember]
        public object LastValue { get; set; }
        [DataMember]
        public object NewValue { get; set; }
    }
}
