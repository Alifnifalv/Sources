using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "MenuLinkTypes")]
    public enum MenuLinkTypes
    {
        [EnumMember]
        TopNavigationOnline = 1,
        [EnumMember]
        LeftNavigationOnline = 2,
        [EnumMember]
        LeftNavigationAdmin = 3,
        [EnumMember]
        ERPNavigation = 4,
        [EnumMember]
        QuickLaunch = 5
    }
}
