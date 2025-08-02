using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public  class LessonPlanTopicAttachmentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
      
        [DataMember]
        public long LessonPlanTopicAttachmentMapIID { get; set; }
        [DataMember]
        public long? LessonPlanTopicMapID { get; set; }
        [DataMember]
        public long? AttachmentReferenceID { get; set; }

        [DataMember]
        public string AttachmentName { get; set; }

        [DataMember]
        public string AttachmentDescription { get; set; }
        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public string UploadFile { get; set; }

    }
}
