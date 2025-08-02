using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ExamSkillMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ExamSkillMapDTO()
        {
            SkillGroup = new KeyValueDTO();
            SkillSet = new KeyValueDTO();
        }

        [DataMember]
        public long ExamSkillMapIID { get; set; }
        [DataMember]
        public long? ExamID { get; set; }
        [DataMember]
        public int? SkillGroupMasterID { get; set; }
        [DataMember]
        public long? ClassSubjectSkillGroupMapID { get; set; }

        [DataMember]
        public string SkillSetName { get; set; }

        [DataMember]
        public string SkillGroupName { get; set; }

        [DataMember]
        public KeyValueDTO SkillGroup { get; set; }
        [DataMember]
        public KeyValueDTO SkillSet { get; set; }

    }
}


