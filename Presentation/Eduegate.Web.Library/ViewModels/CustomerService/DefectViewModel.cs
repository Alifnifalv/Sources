using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.CustomerService
{
    public class DefectViewModel : BaseMasterViewModel
    {
        public DefectViewModel()
        {
            DefectDetails = new List<DefectCodeViewModel>() { new DefectCodeViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid, "","","","",false,"","", true)]
        [DisplayName("")]
        public List<DefectCodeViewModel> DefectDetails { get; set; }
    }
}
