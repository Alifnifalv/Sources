using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class CoScholasticEntryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CoScholasticEntryDTO()
        {
            CoScholasticEntryDetailDTOs = new List<CoScholasticEntryDetailDTO>();
        }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public int? ExamGroupID { get; set; }

        [DataMember]
        public int? SkillGroupID { get; set; }

        [DataMember]
        public int? SkillSetID { get; set; }

        [DataMember]
        public int? SkillID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public List<CoScholasticEntryDetailDTO> CoScholasticEntryDetailDTOs { get; set; }
    }

}