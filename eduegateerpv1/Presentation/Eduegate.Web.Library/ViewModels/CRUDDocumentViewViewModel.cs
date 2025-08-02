using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels.HR;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Document", "CRUDModel.Model.MasterViewModel.Document", "", "header-list", "grid documents", "", true)]
    [DisplayName("Allowance")]
    public class CRUDDocumentViewViewModel : BaseMasterViewModel
    {
        public CRUDDocumentViewViewModel()
        {
            Documents = new List<EmploymentRequestDocumentFileViewModel>() { new EmploymentRequestDocumentFileViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid, "grid documents")]
        [DisplayName(" ")]
        public List<EmploymentRequestDocumentFileViewModel> Documents { get; set; }
    }
}
