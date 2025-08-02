using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class SkillGroupSubjectMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SkillGroupSubjectMapDTO()
        {
            Subject = new List<KeyValueDTO>();
        }

        [DataMember]
        public long SkillGroupSubjectMapIID { get; set; }

        [DataMember]
        public long ClassSubjectSkillGroupMapID { get; set; }

        [DataMember]
        public KeyValueDTO MarkGrade { get; set; }

        [DataMember]
        public int MarkGradeID { get; set; }

        [DataMember]
        public decimal? MinimumMarks { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public KeyValueDTO SkillGroup { get; set; }

        [DataMember]
        public List<KeyValueDTO> Subject { get; set; }

    }
}