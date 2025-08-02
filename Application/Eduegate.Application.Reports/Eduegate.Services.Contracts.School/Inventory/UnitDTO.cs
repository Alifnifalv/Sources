using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Inventory
{
    [DataContract]
    public class UnitDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long UnitID { get; set; }

        [DataMember]
        public string UnitCode { get; set; }

        [DataMember]
        public string UnitName { get; set; }

        [DataMember]
        public double? Fraction { get; set; }

        [DataMember]
        public long? UnitGroupID { get; set; }
    }
}
