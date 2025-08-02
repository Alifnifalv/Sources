using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class VehicleOwnershipTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public short  VehicleOwnershipTypeID { get; set; }
        [DataMember]
        public string  OwnershipTypeName { get; set; }
        [DataMember]
        public string  Description { get; set; }
    }
}


