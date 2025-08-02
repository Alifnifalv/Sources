using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.MenuLinks
{
    [DataContract]
    public class MenuDTO
    {
        [DataMember]
        public long MenuLinkIID { get; set; }
        [DataMember]
        public Nullable<long> ParentMenuID { get; set; }
        [DataMember]
        public string MenuName { get; set; }
        [DataMember]
        public Nullable<byte> MenuLinkTypeID { get; set; }
        [DataMember]
        public string ActionLink { get; set; }
        [DataMember]
        public string ActionLink1 { get; set; }
        [DataMember]
        public string ActionLink2 { get; set; }
        [DataMember]
        public string ActionLink3 { get; set; }
        [DataMember]
        public string Parameters { get; set; }
        [DataMember]
        public Nullable<int> SortOrder { get; set; }
        [DataMember]
        public List<MenuCategoryDTO> MenuCategoryList { get; set; }
        [DataMember]
        public List<BrandDTO1> BrandList { get; set; }
        [DataMember]
        public List<SubMenuDTO> SubMenuList { get; set; }
        [DataMember]
        public string MenuTitle { get; set; }
        [DataMember]
        public string MenuIcon { get; set; }
        [DataMember]
        public List<MenuDTO> MenuList { get; set; }
        [DataMember]
        public string ThumbnailImage { get; set; }
        [DataMember]
        public string BackgroundImage { get; set; }
        [DataMember]
        public Nullable<long> CategoryID { get; set; }
        [DataMember]
        public string CategoryCode { get; set; }
        [DataMember]
        public string MenuGroup { get; set; }
    }
}
