using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    public enum BudgetPeriodTypes
    {
        [EnumMember]
        Periodical = 0,
        [EnumMember]
        Daily = 1,
        [EnumMember]
        Weekly = 2,
        [EnumMember]
        Monthly = 3,
        [EnumMember]
        Quarterly = 4,
        [EnumMember]
        HalfYearly = 5,
        [EnumMember]
        Yearly = 6,
    }
}