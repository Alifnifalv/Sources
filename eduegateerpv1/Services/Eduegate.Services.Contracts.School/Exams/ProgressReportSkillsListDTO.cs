using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ProgressReportSkillsListDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProgressReportSkillsListDTO()
        {
            Exams = new List<ProgressReportExamListDTO>();
        }

        [DataMember]
        public int SkillID { get; set; }

        [DataMember]
        public string SkillName { get; set; }

        [DataMember]
        public List<ProgressReportExamListDTO> Exams { get; set; }
    }
}