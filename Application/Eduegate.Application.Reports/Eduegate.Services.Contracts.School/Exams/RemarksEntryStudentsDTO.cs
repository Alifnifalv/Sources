using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class RemarksEntryStudentsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RemarksEntryStudentsDTO()
        {
            RemarksExam = new List<RemarksEntryExamMapDTO>();
        }

        [DataMember]
        public long RemarksEntryStudentMapIID { get; set; }

        [DataMember]
        public long? RemarksEntryID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string Remarks1 { get; set; }

        [DataMember]
        public string Remarks2 { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public byte? StudentSchoolID { get; set; }

        [DataMember]
        public List<RemarksEntryExamMapDTO> RemarksExam { get; set; }
    }
}


