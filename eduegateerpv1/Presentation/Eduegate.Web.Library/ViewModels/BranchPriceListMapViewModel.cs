using System.ComponentModel;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "PriceLists", "CRUDModel.ViewModel.PriceListMap.PriceLists")]
    [DisplayName("Price Lists")]
    public class BranchPriceListMapViewModel : BaseMasterViewModel
    {
        public long ProductPriceListBranchMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.PriceList")]
        [CustomDisplay("PriceLists")]
        public string PriceListID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Description")]
        public string PriceDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.PriceListMap.PriceLists[0], CRUDModel.ViewModel.PriceListMap.PriceLists)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.PriceListMap.PriceLists[0], CRUDModel.ViewModel.PriceListMap.PriceLists)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}