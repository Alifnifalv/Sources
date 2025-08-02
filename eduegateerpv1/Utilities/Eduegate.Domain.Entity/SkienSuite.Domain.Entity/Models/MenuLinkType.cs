using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MenuLinkType
    {
        public MenuLinkType()
        {
            this.MenuLinks = new List<MenuLink>();
        }

        public byte MenuLinkTypeID { get; set; }
        public string MenuLinkTypeName { get; set; }
        public virtual ICollection<MenuLink> MenuLinks { get; set; }
    }
}
