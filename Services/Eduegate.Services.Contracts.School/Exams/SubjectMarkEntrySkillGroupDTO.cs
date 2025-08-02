using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Exams
{
   public class SubjectMarkEntrySkillGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SubjectMarkEntrySkillGroupDTO()
        {
            MarkRegisterSkillsDTO = new List<MarkRegisterSkillsDTO>();

        }

        [DataMember]
        public long MarkRegisterSkillGroupIID { get; set; }

        [DataMember]
        public long MarkRegisterSubjectMapID { get; set; }

        [DataMember]
        public decimal? MinimumMark { get; set; }

        [DataMember]
        public decimal? MaximumMark { get; set; }

        [DataMember]
        public decimal? MarkObtained { get; set; }

        [DataMember]
        public int? MarksGradeID { get; set; }

        [DataMember]
        public long? MarksGradeMapID { get; set; }

        [DataMember]
        public int? SkillGroupMasterID { get; set; }


        [DataMember]
        public string MarkGradeMap { get; set; }


        [DataMember]
        public string SkillGroup { get; set; }

        [DataMember]
        public List<MarkRegisterSkillsDTO> MarkRegisterSkillsDTO { get; set; }

        [DataMember]
        public bool? IsPassed { get; set; }

        [DataMember]
        public bool? IsAbsent { get; set; }
    }
}
