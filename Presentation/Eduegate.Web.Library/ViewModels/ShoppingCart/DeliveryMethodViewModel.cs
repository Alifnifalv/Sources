using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
   public class DeliveryMethodViewModel
    {
         public int DeliveryMethodID { get; set; }
         public string DeliveryType { get; set; }
         public decimal DeliveryCharge { get; set; }
         public string DeliveryDisplayText { get; set; }

    }
}
