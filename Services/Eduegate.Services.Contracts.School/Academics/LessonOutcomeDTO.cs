using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class LessonOutcomeDTO : BaseMasterDTO
    {
        public LessonOutcomeDTO()
        {
        }

        [DataMember]
        public byte LessonLearningOutcomeID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string LessonLearningOutcomeName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
    }
}