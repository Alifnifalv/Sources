using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "", "detail.SKUDetails")]
    [DisplayName("Serial No#")]
    public class ProductSerialMapViewModel : BaseMasterViewModel
    {
        public long ProductSerialID { get; set; }       
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SerialList", "String", false, onSelectChangeEvent: "OnChangeSerialNo", optionalAttribute1: "ng-show=detail.IsSerialKeyLookUp")]
        [LazyLoad("", "Inventory/GetProductInventorySerialMaps", "LookUps.SerialList")]
        [DisplayName("Serial No")]
        public KeyValueViewModel SerialList { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "ex-large-col-width", "ng-show=!detail.IsSerialKeyLookUp")]
        [DisplayName("")]
        public string SerialNo { get; set; }
        public Nullable<long> DetailID { get; set; }
        public long ProductSKUMapID { get; set; }
        public string PartNo { get; set; }
        public string ErrorMessage { get; set; }
    }
}
