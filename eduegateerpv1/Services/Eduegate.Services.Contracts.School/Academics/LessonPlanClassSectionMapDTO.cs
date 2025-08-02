using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class LessonPlanClassSectionMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LessonPlanClassSectionMapDTO()
        {
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
        }

        [DataMember]
        public long LessonPlanClassSectionMapIID { get; set; }

        [DataMember]
        public long? LessonPlanID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }
    }
}