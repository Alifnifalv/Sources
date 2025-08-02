using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class AssetMasterSummaryViewModel : BaseMasterViewModel
    {        
        [Order(2)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "{{CRUDModel.Model.DetailViewModel|sumByKey:'Quantity'}}")]
        [DisplayName("Total Quantity")]
        public decimal TotalQuantity { get; set; }

        [Order(3)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "{{(CRUDModel.Model.DetailViewModel|sumByKey:'CreditTotal') | number:3}}")]
        [DisplayName("Total Credit")]
        public Nullable<decimal> Credit { get; set; }

        [Order(4)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "{{(CRUDModel.Model.DetailViewModel|sumByKey:'DebitTotal')| number:3}}")] 
        [DisplayName("Total Debit")]
        public Nullable<decimal> Debit { get; set; }
    }
}
