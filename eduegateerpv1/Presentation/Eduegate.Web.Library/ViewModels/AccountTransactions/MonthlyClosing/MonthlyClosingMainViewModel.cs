using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Accounting.MonthlyClosing;
using Eduegate.Web.Library.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MonthlyClosing", "CRUDModel.ViewModel")]
    [DisplayName("Monthly Closing Details")]
    public class MonthlyClosingMainViewModel : BaseMasterViewModel
    {
        public MonthlyClosingMainViewModel()
        {
            MCTabFeeDueGeneral = new MCTabFeeDueGeneralViewModel();
            MCTabInventory = new MCTabInventoryViewModel();
            MCTabFeeVSAccounts = new MCTabFeeVSAccountslViewModel();
            MCTabStock = new MCTabStockViewModel();
            MCTabFeeCancelled = new MCTabFeeCancelledViewModel();
            MCTabAccountsCancelled = new MCTabAccountsCancelledViewModel();
            MCTabInventoryCancelled = new MCTabInventoryCancelledViewModel();
            MCTabMisMatchedFees = new MCTabMisMatchedFeesEntryViewModel();
            MCTabMisMatchedAccounts = new MCTabMisMatchedAccountsInventoryViewModel();
            MCTabAccountsGeneral = new MCTabAccountsGeneralViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change=CheckDateRanges()")]
        [CustomDisplay("From Date")]
        public string StartDateString { get; set; }

        public DateTime? StartDate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change=CheckDateRanges()")]
        [CustomDisplay("To Date")]
        public string EndDateString { get; set; }
        public DateTime? EndDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewMothlyClosingDetails()")]
        [CustomDisplay("View")]
        public string ViewButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=SubmitMothlyClosing()")]
        [CustomDisplay("Submit")]
        public string SubmitButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabFeeDueGeneral", "MCTabFeeDueGeneral")]
        [CustomDisplay("1. Fee General")]
        public MCTabFeeDueGeneralViewModel MCTabFeeDueGeneral { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabFeeVSAccounts", "MCTabFeeVSAccounts")]
        [CustomDisplay("2. Fee VS Accounts")]
        public MCTabFeeVSAccountslViewModel MCTabFeeVSAccounts { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabInventory", "MCTabInventory")]
        [CustomDisplay("3. Inventory")]
        public MCTabInventoryViewModel MCTabInventory { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabStock", "MCTabStock")]
        [CustomDisplay("4. Stock")]
        public MCTabStockViewModel MCTabStock { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabFeeCancelled", "MCTabFeeCancelled")]
        [CustomDisplay("5. Fee Cancelled")]
        public MCTabFeeCancelledViewModel MCTabFeeCancelled { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabAccountsCancelled", "MCTabAccountsCancelled")]
        [CustomDisplay("6. Accounts Cancelled")]
        public MCTabAccountsCancelledViewModel MCTabAccountsCancelled { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabInventoryCancelled", "MCTabInventoryCancelled")]
        [CustomDisplay("7. Inventory Cancelled")]
        public MCTabInventoryCancelledViewModel MCTabInventoryCancelled { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabMisMatchedFees", "MCTabMisMatchedFees")]
        [CustomDisplay("8. Mismatched Entries of Fees")]
        public MCTabMisMatchedFeesEntryViewModel MCTabMisMatchedFees { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabMisMatchedAccounts", "MCTabMisMatchedAccounts")]
        [CustomDisplay("9. Mismatched Entries of Accounts & Inventories")]
        public MCTabMisMatchedAccountsInventoryViewModel MCTabMisMatchedAccounts { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabAccountsGeneral", "MCTabAccountsGeneral")]
        [CustomDisplay("10. Accounts General")]
        public MCTabAccountsGeneralViewModel MCTabAccountsGeneral { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeGeneralMonthlyClosingDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<MonthlyClosingMainViewModel>(jsonString);
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeGeneralMonthlyClosingDTO>(jsonString);
        }

    }
}
