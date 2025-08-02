using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AccountInformations", "CRUDModel.ViewModel.AccountInformations")]
    [DisplayName("Account Information")]
    public class AssetAccountsInfoViewModel : BaseMasterViewModel
    {
        public AssetAccountsInfoViewModel()
        {
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("GLAccount", "String", false)]
        [CustomDisplay("Asset Account")]
        [LookUp("LookUps.AssetAccounts")]
        public KeyValueViewModel GLAccount { get; set; }
        public long? AssetGlAccID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DepreciationGLAccount", "String", false)]
        [CustomDisplay("Accumulated Depreciation Account")]
        [LookUp("LookUps.LiabilityAccounts")]
        public KeyValueViewModel DepreciationGLAccount { get; set; }
        public long? AccumulatedDepGLAccID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ExpenseGLAccount", "String", false)]
        [CustomDisplay("Depreciation Expense Account")]
        [LookUp("LookUps.ExpenseAccounts")]
        public KeyValueViewModel ExpenseGLAccount { get; set; }
        public long? DepreciationExpGLAccId { get; set; }

    }
}