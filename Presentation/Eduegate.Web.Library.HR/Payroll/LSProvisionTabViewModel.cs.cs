using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LSProvisionTab", "CRUDModel.ViewModel.LSProvisionTab")]
    //[Pagination(10, "default")]
    public class LSProvisionTabViewModel : BaseMasterViewModel
    {
        public LSProvisionTabViewModel()
        {
            LSProvisionDetail = new List<LSProvisionDetailsViewModel>() { new LSProvisionDetailsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Details")]
        public List<LSProvisionDetailsViewModel> LSProvisionDetail { get; set; }
    }

}
