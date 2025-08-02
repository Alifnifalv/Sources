using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ProgressReportSkillGroupListDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProgressReportSkillGroupListDTO()
        {
            Exams = new List<ProgressReportExamListDTO>();
            Skills = new List<ProgressReportSkillsListDTO>();
        }

        [DataMember]
        public int SkillGroupID { get; set; }

        [DataMember]
        public string SkillGroupName { get; set; }

        [DataMember]
        public List<ProgressReportExamListDTO> Exams { get; set; }

        [DataMember]
        public List<ProgressReportSkillsListDTO> Skills { get; set; }
    }
}
