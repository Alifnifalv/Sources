using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Logging
{
    public enum ActivityTypes
    {
        [Description("Login")]
        [EnumMember]
        Login = 1,
        [Description("Transactions")]
        [EnumMember]
        Transactions = 2,
        [Description("Document")]
        [EnumMember]
        Document = 2,
    }
}
