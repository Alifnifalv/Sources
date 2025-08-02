using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.MenuLinks
{
    public class MenuLinkItemViewModel
    {
        public long MenuID { get; set; }
        public string MenuName { get; set; }
        public string HtmlAttributes { get; set; }
        public string HtmlAttributes1 { get; set; }
        public string HtmlAttributes2 { get; set; }
        public string HtmlAttributes3 { get; set; }
        public string Parameters { get; set; }
        public string IconClass { get; set; }
        public string IconImage { get; set; }
        public string Title { get; set; }
        public string Hierarchy { get; set; }
        public List<MenuLinkItemViewModel> SubItems { get; set; }
        public bool HasChild { get { return SubItems == null || SubItems.Count == 0 ? false : true; } }
    }
}
