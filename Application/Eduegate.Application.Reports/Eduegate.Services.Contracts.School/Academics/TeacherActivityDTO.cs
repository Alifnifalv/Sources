using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class TeacherActivityDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  TeacherActivityIID { get; set; }
        [DataMember]
        public long?  EmployeeID { get; set; }
        [DataMember]
        public System.DateTime?  ActivityDate { get; set; }
        [DataMember]
        public TimeSpan? TimeFrom { get; set; }
        [DataMember]
        public TimeSpan? TimeTo { get; set; }
        [DataMember]
        public int?  SubjectID { get; set; }
        [DataMember]
        public int?  ClassID { get; set; }
        [DataMember]
        public int?  SectionID { get; set; }
        [DataMember]
        public byte?  ShiftID { get; set; }
        [DataMember]
        public long?  TopicID { get; set; }
        [DataMember]
        public long?  SubTopicID { get; set; }
        [DataMember]
        public byte?  PeriodID { get; set; }
    }
}


