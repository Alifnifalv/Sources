using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Hostel
{
    [DataContract]
    public class HostelDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  HostelID { get; set; }
        [DataMember]
        public string HostelName { get; set; }
        [DataMember]
        public byte?  HostelTypeID { get; set; }
        [DataMember]
        public string  Address { get; set; }
        [DataMember]
        public int?  InTake { get; set; }
        [DataMember]
        public string  Description { get; set; }
    }
}


