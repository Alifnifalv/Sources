using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, " DocumentNo", "CRUDModel.ViewModel.DocumentNumber.DocumentNos")]
    [DisplayName("DocumentNo")]
    public class DocumentNoViewModel : BaseMasterViewModel
    {
        public DocumentNoViewModel()
        {
            DocumentNos = new List<DocumentNosViewModel>() { new DocumentNosViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<DocumentNosViewModel> DocumentNos { get; set; }

    }
}
