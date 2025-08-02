using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.FrontOffices
{
    [DataContract]
    public class AdmissionEnquiryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  AdmissionEnquiryIID { get; set; }
        [DataMember]
        public string  PersonName { get; set; }
        [DataMember]
        public string  PhoneNumber { get; set; }
        [DataMember]
        public string  Email { get; set; }
        [DataMember]
        public string  Address { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public string  Note { get; set; }
        [DataMember]
        public System.DateTime?  Date { get; set; }
        [DataMember]
        public System.DateTime?  NextFollowUpDate { get; set; }
        [DataMember]
        public string  Assinged { get; set; }
        [DataMember]
        public byte?  ReferenceTypeID { get; set; }
        [DataMember]
        public byte?  SourceID { get; set; }
        [DataMember]
        public int?  ClassID { get; set; }
        [DataMember]
        public int?  NumberOfChild { get; set; }
        [DataMember]
        public byte?  SchoolID { get; set; }
    }
}


