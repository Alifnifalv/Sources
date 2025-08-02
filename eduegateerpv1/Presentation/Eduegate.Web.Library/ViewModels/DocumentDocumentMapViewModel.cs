using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DocumentDocumentMap", "CRUDModel.ViewModel.DocumentMap")]
    [DisplayName("Document workflow")]
    public class DocumentDocumentMapViewModel : BaseMasterViewModel
    {
        public DocumentDocumentMapViewModel()
        {
            DocumentMaps = new List<DocumentMapViewModel>() { new DocumentMapViewModel() };
            ApprovalWorkflow = new KeyValueViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.ApprovalWorkflow")]
        [DisplayName("Approval Workflow")]
        [Select2("ApprovalWorkflow", "Numeric", false)]
        public KeyValueViewModel ApprovalWorkflow { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<DocumentMapViewModel> DocumentMaps { get; set; }
    }
}
