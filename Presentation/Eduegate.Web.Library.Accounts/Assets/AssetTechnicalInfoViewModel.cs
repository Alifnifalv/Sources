using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TechnicalInformations", "CRUDModel.ViewModel.TechnicalInformations")]
    [DisplayName("Technical Information")]
    public class AssetTechnicalInfoViewModel : BaseMasterViewModel
    {
        public AssetTechnicalInfoViewModel()
        {
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Supplier", "String", false)]
        [CustomDisplay("Supplier")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Supplier", "LookUps.Supplier")]
        public KeyValueViewModel Supplier { get; set; }
        public long? SupplierID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Bill Number")]
        public string BillNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Bill Date")]
        public string BillDateString { get; set; }

    }
}