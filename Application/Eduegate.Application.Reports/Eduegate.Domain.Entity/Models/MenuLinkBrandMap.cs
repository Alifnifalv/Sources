using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MenuLinkBrandMap
    {
        public long MenuLinkBrandMapIID { get; set; }
        public Nullable<long> MenuLinkID { get; set; }
        public Nullable<long> BrandID { get; set; }
        public string ActionLink { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual MenuLink MenuLink { get; set; }
    }
}
