using System.Runtime.Serialization;

namespace Eduegate.Infrastructure.Enums
{
    [DataContract]
    public enum AccountBehavior
    {
        [EnumMember]
        DebitSide = 1,
        [EnumMember]
        CreditSide = 2,
        [EnumMember]
        Both = 3
    }
}