using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.CustomEntity
{
    public class MenuDetails
    {
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
        //public byte[] TimeStamps { get; set; }
        public string MenuTitle { get; set; }
        public string MenuIcon { get; set; }
    }
}
