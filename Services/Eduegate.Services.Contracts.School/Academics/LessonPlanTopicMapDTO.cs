using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public  class LessonPlanTopicMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long LessonPlanTopicMapsIID { get; set; }

        [DataMember]
        public long? LessonPlanID { get; set; }

        [DataMember]
        public string LectureCode { get; set; }

        [DataMember]
        public string Topic { get; set; }

        [DataMember]
        public int? Period { get; set; }

        [DataMember]
        public virtual List<LessonPlanTopicAttachmentMapDTO> LessonPlanTopicAttachments { get; set; }

        [DataMember]
        public virtual List<LessonPlanTaskMapDTO> LessonPlanTopicTask { get; set; }

    }
}
