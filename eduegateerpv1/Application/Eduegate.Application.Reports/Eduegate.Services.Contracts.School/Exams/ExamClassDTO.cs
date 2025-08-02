using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class ExamClassDTO:Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        //public ExamClassDTO()
        //{
        //    Examscheduledto = new List<ExamScheduleDTO>();
        //}

        [DataMember]
        public long ExamClassMapIID { get; set; }

        [DataMember]
        public long? ExamScheduleID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public string Class { get; set; }
        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string Section { get; set; }

        //[DataMember]

        //public List<ExamScheduleDTO> Examscheduledto { get; set; }


    }
}
