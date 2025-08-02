using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentPromotionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentPromotionDTO()
        {

        }

        [DataMember]
        public long StudentPromotionLogIID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int AcademicYearID { get; set; }
        [DataMember]
        public int ShiftFromAcademicYearID { get; set; }
        [DataMember]
        public long StudentID { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public int ShiftFromClassID { get; set; }
        [DataMember]
        public int ShiftFromSectionID { get; set; }
        [DataMember]
        public int ClassID { get; set; }
        [DataMember]
        public int SectionID { get; set; }
        [DataMember]
        public List<KeyValueDTO> Class { get; set; }
        [DataMember]
        public List<KeyValueDTO> Student { get; set; }

        [DataMember]
        public List<KeyValueDTO> PromoteStudent { get; set; }

        [DataMember]
        public List<KeyValueDTO> ShiftFromClass { get; set; }
        [DataMember]
        public List<KeyValueDTO> ShiftFromSection { get; set; }
        [DataMember]
        public List<KeyValueDTO> Section { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public byte? ShiftFromSchoolID { get; set; }
       
        [DataMember]
        public string PromotionStatus { get; set; }
        [DataMember]
        public byte? PromotionStatusID { get; set; }

        [DataMember]
        public bool? IsPromoted { get; set; }
    }
}