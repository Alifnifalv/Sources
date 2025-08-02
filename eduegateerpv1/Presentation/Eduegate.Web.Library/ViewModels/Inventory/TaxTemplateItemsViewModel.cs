using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Items", "CRUDModel.ViewModel.TemplateItems")]
    [DisplayName("Items")]
    public class TaxTemplateItemsViewModel : BaseMasterViewModel
    {
        public TaxTemplateItemsViewModel()
        {
            Account = new KeyValueViewModel();
        }

        public int TaxTemplateItemID { get; set; }
        public int TaxTemplateID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TaxTypes")]
        [DisplayName("Tax Type")]
        public string TaxTypeID { get; set; }

        public long? AccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("GL Accounts")]
        [Select2("Accounts", "Numeric", false, "", false)]
        [LazyLoad("", "AssetMaster/AccountCodesSearch", "LookUps.Accounts")]
        public KeyValueViewModel Account { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Percentage")]
        public int? Percentage { get; set; }
    }
}
