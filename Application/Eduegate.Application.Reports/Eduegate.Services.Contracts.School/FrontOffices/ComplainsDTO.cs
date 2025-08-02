using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.FrontOffices
{
    [DataContract]
    public class ComplainsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  ComplainIID { get; set; }
        [DataMember]
        public byte?  ComplainTypeID { get; set; }
        [DataMember]
        public byte?  SourceID { get; set; }
        [DataMember]
        public string  ComplainType { get; set; }
        [DataMember]
        public string  Phone { get; set; }
        [DataMember]
        public System.DateTime?  Date { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public string  ActionTaken { get; set; }
        [DataMember]
        public string  Assigned { get; set; }
        [DataMember]
        public string  Note { get; set; }
    }
}


