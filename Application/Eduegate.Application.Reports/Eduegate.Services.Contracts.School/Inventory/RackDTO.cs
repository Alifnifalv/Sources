using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Inventory
{
    [DataContract]
    public class RackDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long RackID { get; set; }
        [DataMember]
        public string RackName { get; set; }
    }
}
