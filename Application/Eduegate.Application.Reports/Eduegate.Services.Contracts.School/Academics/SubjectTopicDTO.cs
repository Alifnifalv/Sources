using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class SubjectTopicDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  SubjectTopicIID { get; set; }
        [DataMember]
        public string  TopicName { get; set; }
        [DataMember]
        public decimal?  Duration { get; set; }
        [DataMember]
        public int?  SubjectID { get; set; }
        [DataMember]
        public int?  ClassID { get; set; }
        [DataMember]
        public int?  SectionID { get; set; }
        [DataMember]
        public long?  EmployeeID { get; set; }
        [DataMember]
        public long?  ParentTopicID { get; set; }
    }
}


