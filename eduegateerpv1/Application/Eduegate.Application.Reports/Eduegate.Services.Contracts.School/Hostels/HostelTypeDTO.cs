using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Hostel
{
    [DataContract]
    public class HostelTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  HostelTypeID { get; set; }
        [DataMember]
        public string  TypeName { get; set; }
    }
}


