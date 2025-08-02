using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "DataTypes")]
    public enum DataTypes
    {
        [Description("Number")] 
        [EnumMember]
        Number = 1,
        [Description("String")] 
        [EnumMember]
        String = 2,
        [Description("Date")] 
        [EnumMember]
        Date = 3,
        [Description("Date time")] 
        [EnumMember]
        DateTime = 4,
        [Description("Time")] 
        [EnumMember]
        Time = 5,
        [Description("CommaSeparated")]
        [EnumMember]
        CommaSeparated = 6,
    }
}
