using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class RemarksEntryExamMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long RemarksEntryExamMapIID { get; set; }

        [DataMember]
        public long? RemarksEntryStudentMapID { get; set; }

        [DataMember]
        public long? ExamID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string ExamName { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

    }
}


