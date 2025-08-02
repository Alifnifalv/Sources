using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class BroadCastDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long BroadcastIID { get; set; }

        [DataMember]
        public string BroadcastName { get; set; }

        [DataMember]
        public Nullable<long> FromLoginID { get; set; }

        [DataMember]
        public long ToLoginID { get; set; }

        [DataMember]
        public  List<long> ToLoginIDs { get; set; }

        [DataMember]
        public List<long?> LoginIDs { get; set; }

        [DataMember]

        public List<CommentDTO> Messages { get; set; }

        [DataMember]

        public long? StudentID { get; set; }

        [DataMember]

        public List<BroadcastRecipientsDTO> Participants { get; set; }
    }
}
