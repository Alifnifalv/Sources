using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MenuLinkCategoryMap
    {
        public long MenuLinkCategoryMapIID { get; set; }
        public Nullable<long> MenuLinkID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string ActionLink { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Category Category { get; set; }
        public virtual MenuLink MenuLink { get; set; }
        public virtual Site Site { get; set; }
    }
}
