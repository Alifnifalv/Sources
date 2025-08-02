using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.Budgeting.Budget
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Budgeting", "CRUDModel.ViewModel")]
    [DisplayName("Budgeting")]
    public class  BudgetingViewModel : BaseMasterViewModel
    {
        public BudgetingViewModel()
        {
            IsSelected = false;
            IsExpand = false;
            IsEdit = false;
            AccountGroup = new KeyValueViewModel();
            BudgetingAccountDetail = new BudgetingAccountDetailViewModel();
            Allocations = new List<AllocationAmountViewModel>() { new AllocationAmountViewModel() };
            HeaderTitles = new List<string>();
            Budget = new KeyValueViewModel();
            BudgetType = new KeyValueViewModel();
            BudgetSuggestion = new KeyValueViewModel();
            CostCenter = new KeyValueViewModel();
            BudgetListModel = new List<BudgetingViewModel>();
            AccountGroupList = new List<KeyValueViewModel>();
        }

        public long BudgetEntryIID { get; set; }

        public bool IsSelected { get; set; }

        public bool IsExpand { get; set; }

        public bool IsEdit { get; set; }

        public KeyValueViewModel Budget {  get; set; }
        public int? BudgetID { get; set; }

        public KeyValueViewModel BudgetType { get; set; }
        public byte? BudgetTypeID { get; set; }

        public KeyValueViewModel BudgetSuggestion { get; set; }
        public byte? BudgetSuggestionID { get; set; }

        public KeyValueViewModel CostCenter { get; set; }
        public int? CostCenterID { get; set; }

        public string FromDateString { get; set; }
        public DateTime? SuggestedStartDate { get; set; }

        public string ToDateString { get; set; }
        public DateTime? SuggestedEndDate { get; set; }

        public KeyValueViewModel AccountGroup { get; set; }
        public int? AccountGroupID { get; set; }

        public List<KeyValueViewModel> AccountGroupList { get; set; }

        public List<string> HeaderTitles { get; set; }

        public decimal? SuggestedValue { get; set; } = 0;

        public decimal? Percentage { get; set; } = 0;

        public decimal? EstimateValue { get; set; } = 0;

        public decimal? Amount { get; set; } = 0;

        public string SuggestedFromDateString { get; set; }

        public string SuggestedToDateString { get; set; }

        public List<AllocationAmountViewModel> Allocations { get; set; }

        public BudgetingAccountDetailViewModel BudgetingAccountDetail { get; set; }

        public List<BudgetingViewModel> BudgetListModel { get; set; }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<BudgetingViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<BudgetEntryDTO, BudgetingViewModel>.CreateMap();
           // Mapper<BudgetingAccountGroupsDTO, BudgetingDetailViewModel >.CreateMap();
           // Mapper<BudgetingAccountsDTO, BudgetingAccountDetailViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var feeDto = dto as BudgetEntryDTO;
            var vm = Mapper<BudgetEntryDTO, BudgetingViewModel>.Map(dto as BudgetEntryDTO);

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<BudgetingViewModel, BudgetEntryDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
           
            var dto = Mapper<BudgetingViewModel, BudgetEntryDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
           
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<BudgetEntryDTO>(jsonString);
        }

    }
}