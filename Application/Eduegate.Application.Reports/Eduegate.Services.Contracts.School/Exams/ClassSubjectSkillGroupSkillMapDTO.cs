using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ClassSubjectSkillGroupSkillMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassSubjectSkillGroupSkillMapDTO()
        {
            SkillMaster = new KeyValueDTO();
            MarkGrade = new KeyValueDTO();
            SkillGroup = new KeyValueDTO();
        }

        [DataMember]
        public long ClassSubjectSkillGroupSkillMapID { get; set; }

        [DataMember]
        public long ClassSubjectSkillGroupMapID { get; set; }

        [DataMember]
        public int SkillMasterID { get; set; }

        [DataMember]
        public KeyValueDTO SkillMaster { get; set; }

        [DataMember]
        public KeyValueDTO MarkGrade { get; set; }

        [DataMember]
        public int MarkGradeID { get; set; }

        [DataMember]
        public decimal? MinimumMarks { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public bool? IsEnableInput { get; set; }

        [DataMember]
        public int? SkillGroupMasterID { get; set; }

        [DataMember]
        public KeyValueDTO SkillGroup { get; set; }

    }
}