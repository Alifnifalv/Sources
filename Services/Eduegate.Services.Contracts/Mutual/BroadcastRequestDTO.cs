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
    public class BroadcastRequestDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]

        public CommentDTO Comment { get; set; }

        [DataMember]


        public List<BroadcastRecipientDTO> BroadcastLoginIDs { get; set; }

    }
}
public class BroadcastRecipientDTO
{
    public long LoginID { get; set; }
    public long? StudentID { get; set; } // nullable if needed
}
