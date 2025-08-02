using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class CommentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CommentIID { get; set; }
        [DataMember]
        public Nullable<long> ParentCommentID { get; set; }
        [DataMember]
        public EntityTypes EntityType { get; set; }
        [DataMember]
        public long ReferenceID { get; set; }
        [DataMember]
        public string CommentText { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public Nullable<long> DepartmentID { get; set; }
        [DataMember]
        public Nullable<long> FromLoginID { get; set; }
        [DataMember]
        public Nullable<long> ToLoginID { get; set; }

        [DataMember]
        public bool IsRead { get; set; }

        [DataMember]
        public string ParentCommentText { get; set; }

        [DataMember]
        public long? BroadcastID { get; set; }


        [DataMember]
        public string BroadcastName { get; set; }

        [DataMember]
        public List<long> RecipientIDs { get; set; }

        [DataMember]
        public Nullable<long> PhotoContentID { get; set; }

        [DataMember]
        public object AdmissionNumber { get; set; }

        [DataMember]
        public object StudentName { get; set; }
    }
}
