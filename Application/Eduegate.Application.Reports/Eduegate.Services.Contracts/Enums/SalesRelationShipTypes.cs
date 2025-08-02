using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "SalesRelationShipTypes")]
    public enum SalesRelationShipTypes
    {
        [Description("All")]
        [EnumMember]
        All = 0,
        [Description("Up-Sell")]
        [EnumMember]
        UpSell = 1,
        [Description("Cross-Sell")]
        [EnumMember]
        CrossSell = 2,
        [Description("Accessory")]
        [EnumMember]
        Accessory = 3,
        [Description("Substitute")]
        [EnumMember]
        Substitute = 4,
    }
}
