using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Checkout
{
    public class CartViewModel
    {
        public string CartID { get; set; }

        public List<OrderViewModel> Orders { get; set; }
    }
}
