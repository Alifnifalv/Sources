using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public  class LessonPlanAttachmentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
      
        [DataMember]
        public long LessonPlanAttachmentMapIID { get; set; }
        [DataMember]
        public long? LessonPlanID { get; set; }
        [DataMember]
        public long? AttachmentReferenceID { get; set; }
        [DataMember]
        public string AttachmentName { get; set; }
        [DataMember]
        public string AttachmentDescription { get; set; }
        [DataMember]
        public string Notes { get; set; }

    }
}
