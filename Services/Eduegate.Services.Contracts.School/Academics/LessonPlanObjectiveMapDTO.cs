using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public  class LessonPlanObjectiveMapDTO : BaseMasterDTO
    {

        [DataMember]
        public long LessonPlanLearningObjectiveMapIID { get; set; }

        [DataMember]
        public long? LessonPlanID { get; set; }

        [DataMember]
        public byte? LessonLearningObjectiveID { get; set; }

        [DataMember]
        public string LessonLearningObjectiveName { get; set; }

        [DataMember]
        public string ObjectiveName { get; set; }
    }
}
