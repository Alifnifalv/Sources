using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.ViewModels.Catalog;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
   public class ShoppingCartCatalogList : CatalogListBase
    {
       public int ProductCartQuantity { get; set; }
       public List<DeliveryMethodViewModel> DeliveryMethod { get; set; }
       public List<KeyValueViewModel> DeliveryOptions { get; set; }
         
       public int DeliveryMethodSelected { get; set; }
       public bool IsOutOfStock { get; set; }
       public bool IsCartQuantityAdjusted { get; set; }
       public bool IntlDeliveryEnabled { get; set; }
       public string DeliveryDisplayText { get; set; }
       public ShoppingCartCatalogList()
       {
           ProductCartQuantity = 0;
           DeliveryMethod = new List<DeliveryMethodViewModel>();
       }
    }
}
