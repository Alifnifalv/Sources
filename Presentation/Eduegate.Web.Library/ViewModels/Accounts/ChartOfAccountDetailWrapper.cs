using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class ChartOfAccountDetailWrapper
    {
        public ChartOfAccountDetailWrapper()
        {
            Details = new List<ChartOfAccountDetailViewModel>();
        }

        [ContainerType(Framework.Enums.ContainerTypes.Grid, "Details", "Details")]
        [DisplayName("")]
        public List<ChartOfAccountDetailViewModel> Details { get; set; }
    }
}
