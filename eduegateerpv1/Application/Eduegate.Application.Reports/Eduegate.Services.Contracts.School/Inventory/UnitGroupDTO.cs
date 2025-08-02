using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Inventory
{
    [DataContract]
    public class UnitGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long UnitGroupID { get; set; }

        [DataMember]
        public string UnitGroupCode { get; set; }

        [DataMember]
        public string UnitGroupName { get; set; }

        [DataMember]
        public double? Fraction { get; set; }

    }
}
