using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "Conditions")]
    public enum Conditions
    {
        [Description("Equals")]
        [EnumMember]
        Equals = 1,
        [Description("Not equals")]
        [EnumMember]
        NotEquals = 2,
        [Description("Lesser than or equal")]
        [EnumMember]
        LesserThanEqual = 3,
        [Description("Greater than or equal")]
        [EnumMember]
        GreaterThanEqual = 4,
        [Description("Contains")]
        [EnumMember]
        Contains = 5,
        [Description("Lesser than ")]
        [EnumMember]
        LesserThan = 6,
        [Description("Greater than")]
        [EnumMember]
        GreaterThan = 7,
        [Description("Between")]
        [EnumMember]
        Between = 8,
        [Description("Not Contains")]
        [EnumMember]
        NotContains = 9,
    }
}
