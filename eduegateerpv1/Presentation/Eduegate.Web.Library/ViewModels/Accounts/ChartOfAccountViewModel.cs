using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [Css("alignleft")]
    public class ChartOfAccountViewModel : BaseMasterViewModel
    {
        public ChartOfAccountViewModel()
        {
            Details = new List<ChartOfAccountDetailViewModel>() { new ChartOfAccountDetailViewModel() };
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("ID")]
        public long ChartOfAccountIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Name")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string ChartName { get; set; }

         [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<ChartOfAccountDetailViewModel> Details { get; set; }

         public static ChartOfAccountDTO ToDTO(ChartOfAccountViewModel vm)
         {
             Mapper<ChartOfAccountViewModel, ChartOfAccountDTO>.CreateMap();
             Mapper<ChartOfAccountDetailViewModel, ChartOfAccountDetailDTO>.CreateMap();
             var mapper = Mapper<ChartOfAccountViewModel, ChartOfAccountDTO>.Map(vm);
             int index = 0;

             foreach (var detail in mapper.Details)
             {
                 detail.AccountID = vm.Details[index].GLAccount != null && vm.Details[index].GLAccount.Key != null ? (long?)int.Parse(vm.Details[index].GLAccount.Key) : null;
                 detail.IncomeOrBalanceID = vm.Details[index].IncomeOrBalance == null ? null : (int?) int.Parse(vm.Details[index].IncomeOrBalance);
                 detail.ChartRowTypeID = vm.Details[index].ChartRowType == null ? null : (int?) int.Parse(vm.Details[index].ChartRowType);

                 if (detail.AccountID.HasValue)
                 {
                     detail.AccountCode = null;
                     detail.ChartRowTypeID = null;
                 }

                 index++;
             }

             return mapper;
         }

         public static ChartOfAccountViewModel ToViewModel(ChartOfAccountDTO dto)
         {
             Mapper<ChartOfAccountDTO, ChartOfAccountViewModel>.CreateMap();
             Mapper<ChartOfAccountDetailDTO, ChartOfAccountDetailViewModel>.CreateMap();
             var mapper = Mapper<ChartOfAccountDTO, ChartOfAccountViewModel>.Map(dto);
             int index = 0;

             foreach (var detail in mapper.Details)
             {
                 detail.GLAccount = new KeyValueViewModel() { Key = dto.Details[index].AccountID.HasValue ? dto.Details[index].AccountID.ToString() : null,
                  Value = dto.Details[index].Name};
                 detail.IncomeOrBalance = dto.Details[index].IncomeOrBalanceID.HasValue ? dto.Details[index].IncomeOrBalanceID.ToString() : null;
                 detail.ChartRowType = dto.Details[index].ChartRowTypeID.HasValue ? dto.Details[index].ChartRowTypeID.ToString() : null;
                 detail.IsGLAccount = dto.Details[index].AccountID.HasValue ? true : false;
                 index++;
             }

             return mapper;
         }
    }
}
