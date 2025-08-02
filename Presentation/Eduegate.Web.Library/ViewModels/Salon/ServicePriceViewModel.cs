using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Saloon
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "PriceDetails", "CRUDModel.Model.PriceSetting.PriceDetails")]
    [DisplayName("")]
    public class ServicePriceViewModel : BaseMasterViewModel
    {
        public ServicePriceViewModel()
        {
            Duration = new KeyValueViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Duration", "Numeric", false)]
        [LookUp("LookUps.Duration")]
        [DisplayName("Duration")]
        public KeyValueViewModel Duration { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Discount Price")]
        public decimal DiscountPrice { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.PriceSetting.PriceDetails[0], CRUDModel.Model.PriceSetting.PriceDetails)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.Model.PriceSetting.PriceDetails[0], CRUDModel.Model.PriceSetting.PriceDetails)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
