using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.FrontOffices
{
    [DataContract]
    public class ComplainTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  ComplainTypeID { get; set; }
        [DataMember]
        public string  ComplainDescription { get; set; }
    }
}


