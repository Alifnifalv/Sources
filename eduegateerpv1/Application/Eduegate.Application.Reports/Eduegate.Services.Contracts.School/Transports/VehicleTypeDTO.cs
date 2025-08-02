using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class VehicleTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public short  VehicleTypeID { get; set; }
        [DataMember]
        public string  VehicleTypeName { get; set; }
        [DataMember]
        public string  Capacity { get; set; }
        [DataMember]
        public string  Dimensions { get; set; }
    }
}


