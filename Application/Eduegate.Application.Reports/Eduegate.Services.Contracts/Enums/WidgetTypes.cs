using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "WidgetTypes")]
    public enum WidgetTypes
    {
        [EnumMember]
        All = 0,
        [EnumMember]
        TopOffers = 1,
        [EnumMember]
        Trending = 2,
        [EnumMember]
        AllOffers = 3,
        [EnumMember]
        Recommended = 4,
        [EnumMember]
        RecentlyViewed = 5,
        [EnumMember]
        CategoryRecommended = 6,
        [EnumMember]
        CategoryDepartment = 7,
        [EnumMember]
        CategoryInterested = 8,
    }
}
