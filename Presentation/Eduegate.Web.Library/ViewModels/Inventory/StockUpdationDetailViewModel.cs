using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using Eduegate.Web.Library.ViewModels.Inventory.Purchase;
using System.Globalization;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StockUpdationDetail", "CRUDModel.ViewModel.StockUpdationDetail")]
    [DisplayName("Student List")]
    public class StockUpdationDetailViewModel : BaseMasterViewModel
    {
        public StockUpdationDetailViewModel()
        {
            PhysicalQuantity = 0;
        }

        public long? ProductID { get; set; }
        public long DetailIID { get; set; }
        public long? HeadID { get; set; }

        public long StockVerficationDetailIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label,"small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("ItemCode")]
        public string ProductCode { get; set; }

        public KeyValueViewModel ProductSKU { get; set; }

        public long? ProductSKUMapID { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        ////[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "initialvalue=0")]
        //[DisplayName("Unit Price")]
        //public double UnitPrice { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[DisplayName("Amount")]
        //public double Amount { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        //[DisplayName("Quantity")]
        //public double StockQuantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width disabled")]
        [DisplayName("Book Stock")]
        public decimal? BookStock { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public double Quantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width disabled")]
        [DisplayName("Physical Stock")]
        public decimal? PhysicalQuantity { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Book Quantity")]
        //public double BookQuantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width disabled")]
        [DisplayName("Diff Quantity")]
        public decimal? DifferenceQuantity { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Physical Value")]
        //public double PhysicalValue { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Book Value")]
        //public double BookValue { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Diff Value")]
        //public double DifferenceValue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("Corrected Quantity")]
        public decimal? CorrectedQuantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        public string Remark { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.StockUpdationDetail[0],CRUDModel.ViewModel.StockUpdationDetail)'")]
        //[DisplayName("+")]
        //public string Add { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.StockUpdationDetail[0],CRUDModel.ViewModel.StockUpdationDetail)'")]
        //[DisplayName("-")]
        //public string Remove { get; set; }

    }
}
