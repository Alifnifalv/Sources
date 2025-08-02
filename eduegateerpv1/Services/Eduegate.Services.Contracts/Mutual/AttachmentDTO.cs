using System;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class AttachmentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long AttachmentIID { get; set; }
        [DataMember]
        public Nullable<long> ParentAttachmentID { get; set; }
        [DataMember]
        public EntityTypes EntityType { get; set; }
        [DataMember]
        public long ReferenceID { get; set; }
        [DataMember]
        public string AttachmentName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public Nullable<long> DepartmentID { get; set; }
    }
}
