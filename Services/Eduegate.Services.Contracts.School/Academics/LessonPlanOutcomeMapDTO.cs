using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public  class LessonPlanOutcomeMapDTO : BaseMasterDTO
    {

        [DataMember]
        public long LessonPlanLearningOutcomeMapIID { get; set; }

        [DataMember]
        public long? LessonPlanID { get; set; }

        [DataMember]
        public byte? LessonLearningOutcomeID { get; set; }

        [DataMember]
        public string LessonLearningOutcomeName { get; set; }

        [DataMember]
        public string OutcomeName { get; set; }
    }
}
