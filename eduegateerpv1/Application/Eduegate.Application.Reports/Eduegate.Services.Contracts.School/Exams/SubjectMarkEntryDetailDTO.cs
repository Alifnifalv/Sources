using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class SubjectMarkEntryDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SubjectMarkEntryDetailDTO()
        {
            MarkRegisterSkillGroupDTO = new List<MarkRegisterSkillGroupDTO>();
        }

        [DataMember]
        public long? MarkRegisterStudentMapID { get; set; }

        [DataMember]
        public long? MarkRegisterID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

       

        [DataMember]
        public bool IsExpand { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public long MarkRegisterSubjectMapIID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public decimal? MinimumMark { get; set; }
        [DataMember]
        public decimal? MaximumMark { get; set; }
        [DataMember]
        public decimal? Mark { get; set; }


        [DataMember]
        public long? MarksGradeMapID { get; set; }
        [DataMember]
        public int? MarksGradeID { get; set; }
        [DataMember]
        public string Grade { get; set; }

        [DataMember]
        public bool? IsPassed { get; set; }

        [DataMember]
        public bool? IsAbsent { get; set; }

        [DataMember]
        public MarkGradeMapDTO MarkGradeDTO { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string MarkGradeMap { get; set; }

        [DataMember]
        public List<MarkRegisterSkillGroupDTO> MarkRegisterSkillGroupDTO { get; set; }
    }
}