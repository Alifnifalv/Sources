using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class MarkRegisterDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public MarkRegisterDetailsDTO()
        {
            Student = new KeyValueDTO();
            MarkRegisterSplitDTO = new List<MarkRegisterDetailsSplitDTO>();
        }

        [DataMember]
        public long MarkRegisterStudentMapIID { get; set; }

        [DataMember]
        public long? MarkRegisterID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string Studentdetails { get; set; }

        [DataMember]
        public bool IsExpand { get; set; }

        [DataMember]
        public byte? MarkStatusID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public List<MarkRegisterDetailsSplitDTO> MarkRegisterSplitDTO { get; set; }

        [DataMember]
        public List<MarkRegisterSkillGroupDTO> MarkRegisterSkillGroupDTO { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public byte? MarkEntryStatusID { get; set; }

        [DataMember]
        public string MarkEntryStatusName { get; set; }

        [DataMember]
        public string FeeDefaulterStatus { get; set; }

        [DataMember]
        public long? ReportContentID { get; set; }
    }
}