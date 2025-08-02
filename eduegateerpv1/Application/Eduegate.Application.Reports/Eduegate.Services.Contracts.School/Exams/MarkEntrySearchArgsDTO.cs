using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{

    [DataContract]
    public class MarkEntrySearchArgsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public string AdmissionNumber { get; set; }
        public long? StudentID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? SkillSetID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? SkillGroupID { get; set; }
        public int? SkillID { get; set; }
        public long TermID { get; set; }
        public long SubjectId { get; set; }
        public long ExamID { get; set; }
        public short? LanguageTypeID { get; set; }
        public int? SubjectMapID { get; set; }

        public string tabType { get; set; }
    }
}
