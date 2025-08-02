using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Mutual

{
    [DataContract]
    public class BroadCastDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO

    {
        [DataMember]

        public long BroadcastID { get; set; }
        [DataMember]

        public string BroadcastName { get; set; }
        [DataMember]

        public long FromLoginID { get; set; }
        [DataMember]

        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public List<BroadcastRecipientsDTO> Participants { get; set; }
    }
}