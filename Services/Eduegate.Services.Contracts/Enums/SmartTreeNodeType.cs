using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "SmartViewType")]
    public enum SmartTreeNodeType
    {
        [EnumMember]
        Root,
        [EnumMember]
        Product,
        [EnumMember]
        Category
    }
}