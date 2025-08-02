using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class StudentSkillRegisterMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentSkillRegisterMapDTO()
        {
            StudentSkillRegisterSkillMapDTO = new List<StudentSkillRegisterSplitDTO>();
            Skill = new KeyValueDTO();
        }

        [DataMember]
        public long StudentSkillRegisterID { get; set; }

        [DataMember]
        public int SkillGroupMasterID { get; set; }

        [DataMember]
        public string SkillGroupMaster { get; set; }

        [DataMember]
        public bool IsExpand { get; set; }

        [DataMember]
        public KeyValueDTO Skill { get; set; }

        [DataMember]
        public decimal? MinimumMarks { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public List<StudentSkillRegisterSplitDTO> StudentSkillRegisterSkillMapDTO { get; set; }
    }
}