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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ESBProvisionTab", "CRUDModel.ViewModel.ESBProvisionTab")]
    //[Pagination(10, "default")]
    public class ESBProvisionTabViewModel : BaseMasterViewModel
    {
        public ESBProvisionTabViewModel()
        {
            ESBProvisionDetail = new List<ESBProvisionDetailsViewModel>() { new ESBProvisionDetailsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Details")]
        public List<ESBProvisionDetailsViewModel> ESBProvisionDetail { get; set; }
    }

}

