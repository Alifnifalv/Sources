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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AllowanceDetails", "CRUDModel.Model.MasterViewModel.Allowance", "", "", "", "", true)]
    [DisplayName("Allowance")]
    public class EmploymentAllowanceViewModel : BaseMasterViewModel
    {
        public EmploymentAllowanceViewModel()
        {
            this.Allowances = new List<AllowanceViewModel>() { new AllowanceViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName(" ")]
        public List<AllowanceViewModel> Allowances { get; set; }
    }
}
