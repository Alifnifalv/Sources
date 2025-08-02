using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    public class DocumentTypeListViewModel : BaseMasterViewModel
    {
        public DocumentTypeListViewModel()
        {
            DocumentTypes = new List<DocumentTypeMapViewModel>() { new DocumentTypeMapViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<DocumentTypeMapViewModel> DocumentTypes { get; set; }
    }
}
