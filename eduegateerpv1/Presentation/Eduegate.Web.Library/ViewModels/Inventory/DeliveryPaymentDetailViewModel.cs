using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryPaymentDetail", "CRUDModel.Model.MasterViewModel.DeliveryPaymentDetail")]
    [DisplayName("Delivery / Payment Detail")]
    public class DeliveryPaymentDetailViewModel : BaseMasterViewModel
    {
        public DeliveryPaymentDetailViewModel()
        {
            //this.TransactionHeadEntitlementMap = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
            DeliveryOption = new KeyValueViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DeliveryOptions", "Numeric", false)]
        [LookUp("LookUps.DeliveryOptions")]
        [DisplayName("Delivery Options")]
        public KeyValueViewModel DeliveryOption { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        //public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMap { get; set; }

    }
}
