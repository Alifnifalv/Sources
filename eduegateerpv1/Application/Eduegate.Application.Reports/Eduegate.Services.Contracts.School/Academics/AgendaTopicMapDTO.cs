using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public  class AgendaTopicMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long AgendaTopicMapIID { get; set; }
        [DataMember]
        public long? AgendaID { get; set; }

        [DataMember]
        public string LectureCode { get; set; }
        [DataMember]
        public string Topic { get; set; }

    }
}
