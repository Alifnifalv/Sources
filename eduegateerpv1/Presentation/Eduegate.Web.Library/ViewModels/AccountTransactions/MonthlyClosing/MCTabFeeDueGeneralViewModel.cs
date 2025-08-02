using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabFeeDueGeneral", "CRUDModel.ViewModel.MCTabFeeDueGeneral")]
    [DisplayName("Fee Due Tab")]
    public class MCTabFeeDueGeneralViewModel : BaseMasterViewModel
    {

        public MCTabFeeDueGeneralViewModel()
        {
            MCGridFeeDueFeeType = new List<MCGridFeeDueFeeTypeViewModel>() { new MCGridFeeDueFeeTypeViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Fee General")]
        public List<MCGridFeeDueFeeTypeViewModel> MCGridFeeDueFeeType { get; set; }
    }
}
