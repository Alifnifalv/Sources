using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class LessonPlanTaskMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long LessonPlanTaskMapIID { get; set; }

        [DataMember]
        public long? LessonPlanTopicMapID { get; set; }

        [DataMember]
        public string Task { get; set; }

        [DataMember]
        public byte? TaskTypeID { get; set; }

        [DataMember]
        public string TaskTypeString { get; set; }

        [DataMember]
        public KeyValueDTO TaskType { get; set; }

        [DataMember]
        public long? LessonPlanID { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public int? TimeDuration { get; set; }

        [DataMember]
        public virtual List<LessonPlanTaskAttachmentMapDTO> LessonPlanTaskAttachment { get; set; }

    }
}
