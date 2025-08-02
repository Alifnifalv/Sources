using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class MarkRegisterSkillsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]

        public long MarkRegisterSkillIID { get; set; }

        [DataMember]
        public long? MarkRegisterSkillGroupID { get; set; }

        [DataMember]
        public long? MarkRegisterID { get; set; }

        [DataMember]
        public int SkillMasterID { get; set; }

        [DataMember]
        public decimal? MarksObtained { get; set; }


        [DataMember]
        public long? MarksGradeMapID { get; set; }

        [DataMember]
        public int? SkillGroupMasterID { get; set; }

        [DataMember]
        public decimal? MinimumMark { get; set; }

        [DataMember]
        public decimal? MaximumMark { get; set; }

        [DataMember]
        public string MarkGradeMap { get; set; }


        [DataMember]
        public int? MarksGradeID { get; set; }
        [DataMember]
        public string SkillGroup { get; set; }

        [DataMember]
        public string Skill { get; set; }

        [DataMember]
        public bool? IsPassed { get; set; }

        [DataMember]
        public bool? IsAbsent { get; set; }

        [DataMember]
        public string Grade { get; set; }

        [DataMember]
        public List<KeyValueDTO> MarkGradeMapList { get; set; }

        [DataMember]
        public List<MarkGradeMapDTO> GradeMarkRangeList { get; set; }

        [DataMember]
        public byte? MarkEntryStatusID { get; set; }

        [DataMember]
        public string MarkEntryStatusName { get; set; }

        [DataMember]
        public decimal? ConvertionFactor { get; set; }

        [DataMember]
        public decimal MaxMark { get; set; }

        [DataMember]
        public decimal? TotalPercentage { get; set; }

        [DataMember]
        public decimal? TotalMark { get; set; }
    }
}