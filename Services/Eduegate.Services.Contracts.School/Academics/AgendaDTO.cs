using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AgendaDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AgendaDTO()
        {
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
            Subject = new KeyValueDTO();
            AcademicYear = new KeyValueDTO();
            AgendaStatus = new KeyValueDTO();
            AgendaTopicMap = new List<AgendaTopicMapDTO>();
            AgendaTaskMap = new List<AgendaTaskMapDTO>();
            SectionList = new List<KeyValueDTO>();
        }

        [DataMember]
        public long AgendaIID { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string School { get; set; }      

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string LessonPlanCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public KeyValueDTO AgendaStatus { get; set; }

        [DataMember]
        public byte? AgendaStatusID { get; set; }

        [DataMember]
        public int? ReferenceID { get; set; }

        [DataMember]
        public DateTime? Date1 { get; set; }

        [DataMember]
        public string Topic { get; set; }

        [DataMember]
        public string LectureCode { get; set; }

        [DataMember]
        public string Date1String { get; set; }

        [DataMember]
        public List<AgendaTopicMapDTO> AgendaTopicMap { get; set; }

        [DataMember]
        public List<AgendaTaskMapDTO> AgendaTaskMap { get; set; }

        [DataMember]
        public bool? IsSendPushNotification { get; set; }

        [DataMember]
        public List<KeyValueDTO> SectionList { get; set; }
    }
}