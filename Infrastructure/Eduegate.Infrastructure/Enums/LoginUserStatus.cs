using System.Runtime.Serialization;

namespace Eduegate.Infrastructure.Enums
{
    [DataContract(Name = "LoginUserStatus")]
    public enum LoginUserStatus
    {
        [EnumMember]
        Active = 1,
        [EnumMember]
        InActive = 2,
        [EnumMember]
        NeedEmailVerification = 3
    }
}