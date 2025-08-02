using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class MenuViewModel
    {
        public long MenuLinkIID { get; set; }
        public Nullable<long> ParentMenuID { get; set; }
        public string MenuName { get; set; }
        public Nullable<byte> MenuLinkTypeID { get; set; }
        public string ActionLink { get; set; }
        public Nullable<int> SortOrder { get; set; }

        public List<CategoryViewModel> listCategoryViewModel { get; set; }
        public List<BrandViewModel> listBrandViewModel { get; set; }
        public List<SubMenuViewModel> listSubMenuViewModel { get; set; }
    }
}
