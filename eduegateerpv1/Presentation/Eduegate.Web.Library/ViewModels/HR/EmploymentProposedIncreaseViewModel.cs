using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.HR
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ProposedIncreaseDetails", "CRUDModel.Model.MasterViewModel.ProposedIncrease", "", "", "", "", true)]
    [DisplayName("Proposed Increase")]
    public class EmploymentProposedIncreaseViewModel : BaseMasterViewModel
    {
        public EmploymentProposedIncreaseViewModel()
        {
            this.ProposedIncreases = new List<ProposedIncreaseViewModel>() { new ProposedIncreaseViewModel(), new ProposedIncreaseViewModel(), new ProposedIncreaseViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName(" ")]
        public List<ProposedIncreaseViewModel> ProposedIncreases { get; set; }
    }
}
