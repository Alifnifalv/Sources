using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MenuLinkCultureData
    {
        public byte CultureID { get; set; }
        public long MenuLinkID { get; set; }
        public string MenuName { get; set; }
        public string MenuTitle { get; set; }

        public string ActionLink { get; set; }

        public string ActionLink1 { get; set; }

        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual MenuLink MenuLink { get; set; }
    }
}
