using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.FrontOffices
{
    [DataContract]
    public class ComplaintSourceTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  ComplaintSourceTypeID { get; set; }
        [DataMember]
        public string  SourceDescription { get; set; }
    }
}


