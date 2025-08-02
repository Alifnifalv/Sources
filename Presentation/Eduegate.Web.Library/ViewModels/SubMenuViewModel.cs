using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class SubMenuViewModel
    {
        public Nullable<long> MenuLinkID { get; set; }
        public Nullable<long> SubMenuID { get; set; }
        public long? SubMenuParentID { get; set; }
        public string SubMenuName { get; set; }
        public string ActionLink { get; set; }
        public Nullable<int> SortOrder { get; set; }
    }
}
