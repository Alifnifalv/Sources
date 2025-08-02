using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Enums;
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
    public class UploadDocumentViewModel : BaseMasterViewModel
    {
        public UploadDocumentViewModel()
        {
            Documents = new List<StudentDocument>() { new StudentDocument() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid, "grid documents")]
        [DisplayName(" ")]
        public List<StudentDocument> Documents { get; set; }
    }
}
