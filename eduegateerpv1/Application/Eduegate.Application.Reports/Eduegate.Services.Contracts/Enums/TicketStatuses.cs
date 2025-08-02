using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum TicketStatuses
    {
        [EnumMember]
        Open = 1,  
        [EnumMember]
        Onhold = 2,
        [EnumMember]
        Reopen = 3,
        [EnumMember]
        Close = 4, 
    }
}
