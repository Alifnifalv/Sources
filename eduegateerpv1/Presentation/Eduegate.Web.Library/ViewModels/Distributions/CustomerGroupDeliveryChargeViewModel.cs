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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "DeliveryCharge", "CRUDModel.ViewModel.CustomerGroupCharge.Charges")]
    [DisplayName("")]
    public class CustomerGroupDeliveryChargeViewModel : BaseMasterViewModel
    {
        public int DeliveryTypeID { get; set; }

        public int CustomerGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("CustomerGroup", "Numeric", false)]
        [DisplayName("Customer Group")]
        [LookUp("LookUps.CustomerGroup")]
        public KeyValueViewModel CustomerGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("Cart Total From")]
        public Nullable<int> CartTotalLimitFrom { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("Cart Total To")]
        public Nullable<int> CartTotalLimitTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [DisplayName("Delivery Charge")]
        public Nullable<decimal> DeliveryCharge { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [DisplayName("Delivery Charge %")]
        public Nullable<decimal> DeliveryChargePercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='InsertGridRow($index, ModelStructure.CustomerGroupCharge.Charges[0], CRUDModel.ViewModel.CustomerGroupCharge.Charges)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='RemoveGridRow($index, ModelStructure.CustomerGroupCharge.Charges[0], CRUDModel.ViewModel.CustomerGroupCharge.Charges)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
