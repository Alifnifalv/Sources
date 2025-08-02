using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public  class ProgressReportNewDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProgressReportNewDTO()
        {
            PublishStatus = new KeyValueDTO();
        }

        [DataMember]
        public long ProgressReportIID { get; set; }

        [DataMember]
        public long? StudentId { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? PublishStatusID { get; set; }

        [DataMember]
        public long? ReportContentID { get; set; }

        [DataMember]
        public string StudentFullName { get; set; }

        [DataMember]
        public KeyValueDTO PublishStatus { get; set; }

        [DataMember]
        public string ContentFileName { get; set; }

        [DataMember]
        public byte[] ContentData { get; set; }

        [DataMember]
        public long? ReferenceID { get; set; }
    }
}