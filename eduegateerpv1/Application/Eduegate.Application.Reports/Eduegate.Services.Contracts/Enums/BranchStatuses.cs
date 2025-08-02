using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    /// <summary>
    /// This enum represents table: [mutual].[BranchStatuses]
    /// </summary>
    [DataContract(Name = "BranchStatuses")]
    public enum BranchStatuses
    {
        [EnumMember]
        Active = 1,
        [EnumMember]
        InActive = 2
    }
}



