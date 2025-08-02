using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ProductMap", "CRUDModel.ViewModel.ProductMap")]
    [DisplayName("Product Details")]
    public class DeliveryTypeProductViewModel : BaseMasterViewModel
    {
        public DeliveryTypeProductViewModel()
        {
            ProductDetails = new List<DeliveryTypeProductDetailViewModelcs>() { new DeliveryTypeProductDetailViewModelcs() };
        }
      
        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [DisplayName("")]
        public List<DeliveryTypeProductDetailViewModelcs> ProductDetails { get; set; }
    }
}
