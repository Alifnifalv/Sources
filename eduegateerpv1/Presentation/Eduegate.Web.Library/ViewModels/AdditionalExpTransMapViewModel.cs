using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AdditionalExpTransMaps", "CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps")]
    [DisplayName("Additional Expenses")]
    public class AdditionalExpTransMapViewModel : BaseMasterViewModel
    {
        public AdditionalExpTransMapViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", "ng-change=SetProvisionalAccount(gridModel)")]
        [DisplayName("IsAffectSupplier")]
        public bool? IsAffectSupplier { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [DisplayName("AdditionalExpenses")]
        [LookUp("LookUps.AdditionalExpenses")]
        [Select2("AdditionalExpenses", "Numeric", false, "OnAdditionalExpenseChange(gridModel)", false)]
        public KeyValueViewModel AdditionalExpense { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [DisplayName("ProvisionalAccount")]
        [LookUp("LookUps.ProvisionalAccount")]
        [Select2("ProvisionalAccount", "Numeric", false, "ng-disabled=gridModel.IsAffectSupplier")]
        public KeyValueViewModel ProvisionalAccount { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [DisplayName("Currency")]
        [LookUp("LookUps.Currencies")]
        [Select2("Currencies", "Numeric", false, "OnCurrencyChange(gridModel)", false)]
        public KeyValueViewModel Currency { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "ng-blur=GetLocalAmount(gridModel)")]
        [DisplayName("Exchange Rate")]
        public Nullable<decimal> ExchangeRate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", "ng-blur=GetLocalAmount(gridModel)")]
        [DisplayName("ForeignAmount")]
        public Nullable<decimal> ForeignAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]//, Attributes = "ng-bind=GetLocalAmount(gridModel) | number")]
        //[DisplayName("Amount")]
        [DisplayName("LocalAmount")]
        public Nullable<decimal> LocalAmount { get; set; }

        public byte? FiscalYearID { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public long? RefInventoryTransactionHeadID { get; set; }

        public long? RefAccountTransactionHeadID { get; set; }

        public long AdditionalExpensesTransactionsMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, CRUDModel.MasterViewModel.AdditionalExpTransMaps[0], CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveAddExpenseGridRow($index, CRUDModel.MasterViewModel.AdditionalExpTransMaps[0], CRUDModel.Model.MasterViewModel.AdditionalExpTransMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public static AdditionalExpTransMapViewModel ToVm(AdditionalExpensesTransactionsMapDTO dto)
        {
            //Mapper<AdditionalExpenseMapDTO, AdditionalExpenseMapViewModel>.CreateMap();
            //var vm = Mapper<AdditionalExpenseMapDTO, AdditionalExpenseMapViewModel>.Map(dto);
            //return vm;
            var vm = new AdditionalExpTransMapViewModel();
            if (dto != null)
            {
                vm.AdditionalExpense = new KeyValueViewModel() { Key = dto.AdditionalExpenseID.ToString(), Value = dto.AdditionalExpense };
                vm.ProvisionalAccount = new KeyValueViewModel() { Key = dto.ProvisionalAccountID.ToString(), Value = dto.ProvisionalAccount };
                vm.Currency = new KeyValueViewModel() { Key = dto.ForeignCurrencyID.ToString(), Value = dto.Currency }; ;
                vm.ExchangeRate = dto.ExchangeRate;
                vm.ForeignAmount = dto.ForeignAmount;
                vm.LocalAmount = dto.LocalAmount;
                vm.IsAffectSupplier = dto.ISAffectSupplier.HasValue ? dto.ISAffectSupplier : false;
                return vm;
            }
            else return null;
        }

        public static AdditionalExpensesTransactionsMapDTO ToDto(AdditionalExpTransMapViewModel vm)
        {
            //Mapper<AdditionalExpenseMapViewModel, AdditionalExpenseMapDTO>.CreateMap();
            //var dto = Mapper<AdditionalExpenseMapViewModel, AdditionalExpenseMapDTO>.Map(vm);
            //return dto;
            var dto = new AdditionalExpensesTransactionsMapDTO();
            if (vm != null)
            {

                dto.AdditionalExpenseID = vm.AdditionalExpense != null ? Convert.ToByte(vm.AdditionalExpense.Key) : default(byte);
                dto.ForeignCurrencyID = vm.Currency != null ? Convert.ToByte(vm.Currency.Key) : default(byte);
                dto.ExchangeRate = vm.ExchangeRate;
                dto.ForeignAmount = vm.ForeignAmount;
                dto.LocalAmount = vm.LocalAmount;
                return dto;
            }
            else return null;
        }
    }
}
