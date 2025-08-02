using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ProgressReportSubjectListDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProgressReportSubjectListDTO()
        {
            Exams = new List<ProgressReportExamListDTO>();
            SkillGroups = new List<ProgressReportSkillGroupListDTO>();
            //SkillList = new List<SkillGroupDTO>();
        }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public List<ProgressReportExamListDTO> Exams { get; set; }

        [DataMember]
        public List<ProgressReportSkillGroupListDTO> SkillGroups { get; set; }

        //[DataMember]
        //public List<SkillGroupDTO> SkillList { get; set; }
    }
}