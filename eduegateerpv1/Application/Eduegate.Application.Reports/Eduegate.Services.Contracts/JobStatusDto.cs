using System;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class JobStatusDto
    {
        [DataMember]
        public int JobStatusID { get; set; }
        [DataMember]
        public Nullable<int> JobTypeID { get; set; }
        [DataMember]
        public Nullable<int> SerNo { get; set; }
        [DataMember]
        public string StatusName { get; set; }
    }
}
