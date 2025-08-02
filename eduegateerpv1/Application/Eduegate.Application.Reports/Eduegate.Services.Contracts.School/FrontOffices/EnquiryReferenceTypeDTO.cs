using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.FrontOffices
{
    [DataContract]
    public class EnquiryReferenceTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  EnquiryReferenceTypeID { get; set; }
        [DataMember]
        public string  ReferenceName { get; set; }
    }
}


