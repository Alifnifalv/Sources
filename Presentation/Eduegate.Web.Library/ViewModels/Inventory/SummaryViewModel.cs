using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SummaryViewModel : BaseMasterViewModel
    {
        [Order(1)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "{{CRUDModel.Model.MasterViewModel.TransactionNo}}")]
        [DisplayName("Transaction No")]
        public string TransactionNo { get; set; }

        [Order(2)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "{{CRUDModel.Model.DetailViewModel|sumByKey:'Quantity'}}")]
        [DisplayName("Total Quantity")]
        public decimal TotalQuantity { get; set; }
    }
}
