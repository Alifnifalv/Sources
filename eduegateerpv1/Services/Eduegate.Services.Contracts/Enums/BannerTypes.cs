using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "BannerTypes")]
    public enum BannerTypes
    {
        [Description("Home Page")]
        [EnumMember]
        HomePage = 1,
        [Description("Category")]
        [EnumMember]
        Category = 2,
        [Description("Product")]
        [EnumMember]
        Product = 3,
        [Description("Custom")]
        [EnumMember]
        Custom = 4,
        [Description("Home Page Right")]
        [EnumMember]
        HomePageRight = 5,
        [Description("Home Page Ar")]
        [EnumMember]
        HomePageAr = 6,
        [Description("Home Page Right Ar")]
        [EnumMember]
        HomePageRightAr = 7,
    }
}
