using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AreaDetails", "CRUDModel.ViewModel.AreaMap.AreaDetails")]
    [DisplayName("")]
    public class DeliveryAreaDetailViewModel : BaseMasterViewModel
    {
        public DeliveryAreaDetailViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("Area", "Numeric", false)]
        [DisplayName("Area")]
        [LookUp("LookUps.Area")]
        public KeyValueViewModel Area { get; set; }

        public long DelivertyTypeID { get; set; }
        public long AreaID { get; set; }

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
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Delivery Charge %")]
        public Nullable<decimal> DeliveryChargePercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.AreaMap.AreaDetails[0], CRUDModel.ViewModel.AreaMap.AreaDetails)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.AreaMap.AreaDetails[0], CRUDModel.ViewModel.AreaMap.AreaDetails)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
