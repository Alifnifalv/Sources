using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class CoScholasticEntryDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CoScholasticEntryDetailDTO()
        {

        }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public byte? MarkStatusID { get; set; }

        [DataMember]
        public int? SkillGroupMasterID { get; set; }

        [DataMember]
        public int? SkillMasterID { get; set; }

        [DataMember]
        public long? MarksGradeMapID { get; set; }

        [DataMember]
        public long? MarkRegisterSkillGroupID { get; set; }

        [DataMember]
        public long MarkRegisterSkillIID { get; set; }

        [DataMember]
        public long? MarkRegisterID { get; set; }

        [DataMember]
        public decimal? MarksObtained { get; set; }
    }

}