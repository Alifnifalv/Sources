using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract]
    public enum ImageTypes
    {
        [EnumMember]
        Thumbnail = 1,
        [EnumMember]
        Small = 2,
        [EnumMember]
        Large = 3,
        [EnumMember]
        Background = 4,
        [EnumMember]
        HomePageBanner = 5,
        [EnumMember]
        CategoryBanner_Small = 6,
        [EnumMember]
        CategoryBanner_Large = 7,
        [EnumMember]
        Listing = 8,
        [EnumMember]
        Listing_Large = 9,
        [EnumMember]
        Listing_Small = 10,
        [EnumMember]
        CategoryBanner_Small_AR = 11,
        [EnumMember]
        HomePageBanner_AR = 12,
        [EnumMember]
        Category_Sliding = 13,
    }
}
