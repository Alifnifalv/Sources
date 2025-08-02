using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class LessonObjectiveDTO : BaseMasterDTO
    {
        public LessonObjectiveDTO()
        {
     
        }

        [DataMember]
        public byte LessonLearningObjectiveID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string LessonLearningObjectiveName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
    }
}