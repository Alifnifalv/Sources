using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DepreciationInformations", "CRUDModel.ViewModel.DepreciationInformations")]
    [DisplayName("Depreciation Information")]
    public class AssetDepreciationInfoViewModel : BaseMasterViewModel
    {
        public AssetDepreciationInfoViewModel()
        {
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Expected Useful Life")]
        public string ExpectedLife { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Rate of Depreciation (Percentage)")]
        public string DepreciationRate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Expected net realisable scrap value")]
        public string ExpectedScrapValue { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Accumulated depreciation")]
        public string AccumulatedDepreciationRate { get; set; }

    }
}