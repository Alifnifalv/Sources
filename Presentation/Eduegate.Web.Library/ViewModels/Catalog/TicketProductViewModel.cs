using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "TicketProductSKU", "CRUDModel.ViewModel.TicketProductSKUs")]
    [DisplayName("")]
    public class TicketProductViewModel : BaseMasterViewModel
    {
        public TicketProductViewModel()
        {
            SKUID = new KeyValueViewModel();
        }

        public long TicketProductMapID { get; set; }

        public long ProductID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SKUID", "String", false, "OnChangeTicketSKUSelect2")]
        [DisplayName("SKU")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        public KeyValueViewModel SKUID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        [DisplayName("SKU Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left")]
        [DisplayName("Reason")]
        [LookUp("LookUps.TicketReasons")]
        public string Reason { get; set; }

        [RegularExpression("[0-9]{1,}", ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright serialnum-wrap ng-change='UpdateDetailGridValues(''@property.Name'',gridModel, {{$index + 1}})'")]
        [DisplayName("Quantity")]
        public Nullable<decimal> Quantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        [DisplayName("Amount")]
        public string UnitPrice { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Narration")]
        public string Narration { get; set; }

        public long TicketID { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Button, "fixed-width-18px", "ng-click='InsertGridRow($index, CRUDModel.ViewModel.TicketProductSKUs[0], CRUDModel.ViewModel.TicketProductSKUs)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "fixed-width-18px", "ng-click='RemoveGridRow($index, CRUDModel.ViewModel.TicketProductSKUs[0], CRUDModel.ViewModel.TicketProductSKUs)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}