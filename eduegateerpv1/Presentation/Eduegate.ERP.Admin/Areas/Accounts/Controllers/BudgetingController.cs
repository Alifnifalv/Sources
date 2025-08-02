using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    public class BudgetingController : BaseController
    {
        [HttpPost]
        public ActionResult GetBudgetingData(BudgetingViewModel budgetingViewModel)
        {

            var viewModelList = new List<BudgetingDetailViewModel>();
            var budgetingDTO = new BudgetingDTO();
            budgetingDTO.AccountGroups = new List<KeyValueDTO>();
            var data = budgetingViewModel.AccountGroup.Select(x => new KeyValueDTO() { Key = x.Key, Value = x.Value }).ToList();

            budgetingDTO.AccountGroups.AddRange(data);

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetBudgetingData(budgetingDTO);
            viewModelList = GetBudgetingAccountGroupDetails(entry);
            var jsonResult = Json(viewModelList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        private List<BudgetingDetailViewModel> GetBudgetingAccountGroupDetails(List<BudgetingAccountGroupsDTO> listData)
        {
            
            var details = listData
           .GroupBy(a => new { a.AccountGroupID, a.AccountGroup,a.BudgetingAccounts })
           .Select(group => new BudgetingDetailViewModel
           {
               AccountGroupID = group.Key.AccountGroupID,
               AccountGroup = group.Key.AccountGroup,
             
               BudgetingAccountDetail = GetBudgetingAccountDetails(group.Key.BudgetingAccounts)
           })
           .ToList();

            return details;
        }


        private List<BudgetingAccountDetailViewModel> GetBudgetingAccountDetails(List<BudgetingAccountsDTO> listData)
        {

            var details = listData
          .GroupBy(a => new { a.AccountID, a.AccountName })
          .Select(group => new BudgetingAccountDetailViewModel
          {
              AccountID = group.Key.AccountID,
              AccountName = group.Key.AccountName,
              EstimateValue = (decimal?)0.000,
              PercentageValue = (decimal?)0.000,
              Amount1 = (decimal?)0.000,
              Amount2 = (decimal?)0.000,
              Amount3 = (decimal?)0.000,
              Amount4 = (decimal?)0.000,
              Amount5 = (decimal?)0.000,
              Amount6 = (decimal?)0.000,
              Amount7 = (decimal?)0.000,
              Amount8 = (decimal?)0.000,
              Amount9 = (decimal?)0.000,
              Amount10 = (decimal?)0.000,
              Amount11 = (decimal?)0.000,
              Amount12 = (decimal?)0.000
          })
          .ToList();

            return details;
        }
    }
}