using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class StudentSkillRegisterSplitDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        [Key]
        public long StudentSkillRegisterMapIID { get; set; }

        [DataMember]
        public long StudentSkillRegisterID { get; set; }

        [DataMember]
        public int? SkillMasterID { get; set; }

        [DataMember]
        public decimal? Mark { get; set; }

        [DataMember]
        public long? MarksGradeMapID { get; set; }

        [DataMember]
        public string MarksGradeMap { get; set; }

        [DataMember]
        public int MarkGradeID { get; set; }

        [DataMember]
        public decimal? MinimumMarks { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public MarkGradeMapDTO MarkGradeDTO { get; set; }

        [DataMember]
        public string SubSkill { get; set; }

        [DataMember]
        public string MarkGrade { get; set; }
    }
}