using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Scheduler
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SchedulerInfo", "CRUDModel.ViewModel.SchedulerInfo")]
    [DisplayName("Schedulers")]
    public class SchedulerInfoViewModel : BaseMasterViewModel
    {
        public SchedulerInfoViewModel()
        {
            Schedulers = new List<SchedulerGridViewModel>() { new SchedulerGridViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<SchedulerGridViewModel> Schedulers { get; set; }
    }
}
