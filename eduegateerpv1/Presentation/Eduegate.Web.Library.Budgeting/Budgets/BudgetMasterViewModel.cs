using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.Budgeting.Budgets
{
    public class BudgetMasterViewModel : BaseMasterViewModel
    {
        public BudgetMasterViewModel()
        {
            Department = new KeyValueViewModel();
            Currency = new KeyValueViewModel();

        }
        public int BudgetID { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        //[CustomDisplay("Budget Code")]
        public string BudgetCode { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Budget Name")]
        public string BudgetName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Budget Type")]
        [LookUp("LookUps.BudgetTypes")]
        public string BudgetType { get; set; }
        public byte? BudgetTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Budget Group")]
        [LookUp("LookUps.BudgetGroups")]
        public string BudgetGroup { get; set; }
        public byte? BudgetGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Period Start")]
        public string PeriodStartString { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Period End")]
        public string PeriodEndString { get; set; }

        public DateTime? PeriodStart { get; set; }

        public DateTime? PeriodEnd { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Company")]
        [LookUp("LookUps.Companies")]
        public string Company { get; set; }
        public int? CompanyID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Financial Year")]
        [LookUp("LookUps.FinancialYears")]
        public string FinancialYear { get; set; }
        public int? FinancialYearID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Department", "Numeric", false)]
        [LookUp("LookUps.Department")]
        [CustomDisplay("Department")]
        public KeyValueViewModel Department { get; set; }
        public long? DepartmentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }
      
        public int? CurrencyID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [LookUp("LookUps.Currency")]
        [CustomDisplay("Currency")]
        public KeyValueViewModel Currency { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(18, ErrorMessage = "Maximum Length should be within 18!")]
        //[CustomDisplay("Budget Total Value")]
        public decimal? BudgetTotalValue { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("BudgetStatus")]
        [LookUp("LookUps.BudgetStatuses")]
        public string BudgetStatus { get; set; }
        public byte? BudgetStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine13 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

     


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as BudgetMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<BudgetMasterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<BudgetMasterDTO, BudgetMasterViewModel>.CreateMap();

            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var stDtO = dto as BudgetMasterDTO;
            var vm = Mapper<BudgetMasterDTO, BudgetMasterViewModel>.Map(stDtO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.PeriodStartString = stDtO.PeriodStart.HasValue ? stDtO.PeriodStart.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.PeriodEndString = stDtO.PeriodEnd.HasValue ? stDtO.PeriodEnd.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Department = new KeyValueViewModel() { Key = stDtO.Department.Key, Value = stDtO.Department.Value };
            vm.Currency = new KeyValueViewModel() { Key = stDtO.Currency.Key, Value = stDtO.Currency.Value };
            vm.BudgetTotalValue = stDtO.BudgetTotalValue;
            vm.BudgetStatusID = stDtO.BudgetStatusID;
            vm.BudgetStatus = stDtO.BudgetStatusID.ToString();
            vm.CompanyID = stDtO.CompanyID;
            vm.Company = stDtO.CompanyID.ToString();
            vm.FinancialYearID = stDtO.FinancialYearID;
            vm.FinancialYear = stDtO.FinancialYearID.ToString();
            vm.BudgetTypeID = stDtO.BudgetTypeID;
            vm.BudgetType = stDtO.BudgetTypeID.ToString();
            vm.BudgetGroupID = stDtO.BudgetGroupID;
            vm.BudgetGroup= stDtO.BudgetGroupID.ToString();
            vm.BudgetID = stDtO.BudgetID;
            vm.Remarks = stDtO.Remarks;

            return vm;

        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<BudgetMasterViewModel, BudgetMasterDTO>.CreateMap();
            var dto = Mapper<BudgetMasterViewModel, BudgetMasterDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.PeriodStart = string.IsNullOrEmpty(PeriodStartString) ? null : DateTime.ParseExact(PeriodStartString, dateFormat, CultureInfo.InvariantCulture);
            dto.PeriodEnd = string.IsNullOrEmpty(PeriodEndString) ? null : DateTime.ParseExact(PeriodEndString, dateFormat, CultureInfo.InvariantCulture);
            dto.DepartmentID = Department == null || string.IsNullOrEmpty(Department.Key) ? (int?)null : int.Parse(Department.Key);
            dto.CurrencyID = Currency == null || string.IsNullOrEmpty(Currency.Key) ? null : int.Parse(Currency.Key);
            dto.BudgetTotalValue = BudgetTotalValue;
            dto.BudgetStatusID = byte.Parse(BudgetStatus);
            dto.CompanyID = int.Parse(Company);
            dto.FinancialYearID = byte.Parse(FinancialYear);
            dto.BudgetGroupID = byte.Parse(BudgetGroup);
            dto.BudgetTypeID = byte.Parse(BudgetType);
            dto.BudgetID = BudgetID;
            dto.Remarks = Remarks;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<BudgetMasterDTO>(jsonString);
        }

    }
}