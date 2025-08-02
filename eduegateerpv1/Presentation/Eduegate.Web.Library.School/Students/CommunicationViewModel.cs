using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Communication", "CRUDModel.ViewModel.Communication")]
    [DisplayName("Communication")]
    public class CommunicationViewModel : BaseMasterViewModel
    {
        public CommunicationViewModel()
        {
            CommunicationGrid = new List<CommunicationGridViewModel>() { new CommunicationGridViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<CommunicationGridViewModel> CommunicationGrid { get; set; }

    }
}
