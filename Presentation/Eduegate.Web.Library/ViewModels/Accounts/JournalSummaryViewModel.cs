using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class JournalSummaryViewModel : BaseMasterViewModel
    {
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
