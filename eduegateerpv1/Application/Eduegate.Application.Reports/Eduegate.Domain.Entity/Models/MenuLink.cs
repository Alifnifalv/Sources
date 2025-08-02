using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MenuLink :  ISecuredEntity
    {
        public MenuLink()
        {
            this.MenuLinkCategoryMaps = new List<MenuLinkCategoryMap>();
            //this.MenuLinks1 = new List<MenuLink>();
            this.MenuLinkBrandMaps = new List<MenuLinkBrandMap>();
            this.MenuLinkCultureDatas = new List<MenuLinkCultureData>();
        }

        public long MenuLinkIID { get; set; }
        public Nullable<long> ParentMenuID { get; set; }
        public string MenuName { get; set; }
        public Nullable<byte> MenuLinkTypeID { get; set; }
        public string ActionLink { get; set; }
        public string ActionLink1 { get; set; }
        public string ActionLink2 { get; set; }
        public string ActionLink3 { get; set; }
        public string Parameters { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string MenuTitle { get; set; }
        public string MenuIcon { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string MenuGroup { get; set; }

        public virtual ICollection<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }
        public virtual ICollection<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }
        //public virtual ICollection<MenuLink> MenuLinks1 { get; set; }
        //public virtual MenuLink MenuLink1 { get; set; }
        public virtual ICollection<MenuLinkBrandMap> MenuLinkBrandMaps { get; set; }

        public long GetIID()
        {
            return this.MenuLinkIID;
        }
    }
}
