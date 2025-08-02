using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Logging
{
    [DataContract]
    public class ActivityDTO
    {
        [DataMember]
        public long ActivityID { get; set; }
        [DataMember]
        public int ActivityTypeID { get; set; }
        [DataMember]
        public string ActivityTypeName { get; set; }
        [DataMember]
        public int? ActionTypeID { get; set; }
        [DataMember]
        public string ActionTypeName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public long? CreatedBy { get; set; }
        [DataMember]
        public string CreatedUserName { get; set; }
        [DataMember]
        public int ActionStatusID { get; set; }
        [DataMember]
        public string ActionStatusName { get; set; }
        [DataMember]
        public string ReferenceID { get; set; }
        [DataMember]
        public string CreatedDateString { get; set; }

        [DataMember]
        public string EnqueueDateString { get; set; }
    }
}
