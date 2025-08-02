using Eduegate.Framework.Mvc.Attributes;
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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Prospectus", "CRUDModel.ViewModel.Prospectus")]
    [DisplayName("Prospectus")]
    public class ProspectusViewModel : BaseMasterViewModel
    {
        public ProspectusViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }
        public long ProspectusIID { get; set; }

        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Pros No.")]
        public string ProsNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public long NewLine1 { get; set; }

        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Pros Fee")]
        public decimal? ProsFee { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Remarks")]
        public string ProsRemarks { get; set; }

    }
}
