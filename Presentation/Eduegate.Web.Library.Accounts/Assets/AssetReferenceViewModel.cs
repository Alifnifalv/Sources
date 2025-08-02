using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetReferences", "CRUDModel.ViewModel.AssetReferences")]
    [DisplayName("Reference and Notes")]
    public class AssetReferenceViewModel : BaseMasterViewModel
    {
        public AssetReferenceViewModel()
        {

        }

        public long AssetEntryHeadID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Reference")]
        public string Reference { get; set; }

    }
}