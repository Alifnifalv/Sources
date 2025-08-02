using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MenuLink
    {
        public MenuLink()
        {
            this.MenuLinks1 = new List<MenuLink>();
        }

        public long MenuLinkIID { get; set; }
        public Nullable<long> ParentMenuID { get; set; }
        public string MenuName { get; set; }
        public Nullable<byte> MenuLinkTypeID { get; set; }
        public string ActionType { get; set; }
        public string ActionLink { get; set; }
        public string ActionLink1 { get; set; }
        public string ActionLink2 { get; set; }
        public string ActionLink3 { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string MenuTitle { get; set; }
        public string MenuIcon { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public virtual ICollection<MenuLink> MenuLinks1 { get; set; }
        public virtual MenuLink MenuLink1 { get; set; }
        public virtual MenuLinkType MenuLinkType { get; set; }
    }
}
