using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.FrontOffices
{
    [DataContract]
    public class VisitorBookDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  VisitorBookIID { get; set; }
        [DataMember]
        public byte?  VisitingPurposeID { get; set; }
        [DataMember]
        public string  VisitorName { get; set; }
        [DataMember]
        public string  PhoneNumber { get; set; }
        [DataMember]
        public string  IDCard { get; set; }
        [DataMember]
        public string  NumberOfPerson { get; set; }
        [DataMember]
        public System.DateTime?  Date { get; set; }
        [DataMember]
        public System.TimeSpan?  InTime { get; set; }
        [DataMember]
        public System.TimeSpan?  OutTime { get; set; }
        [DataMember]
        public string  Note { get; set; }
    }
}


