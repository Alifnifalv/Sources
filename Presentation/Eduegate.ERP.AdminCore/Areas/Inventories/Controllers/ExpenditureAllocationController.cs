using Eduegate.Application.Mvc;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
namespace Eduegate.ERP.AdminCore.Areas.Inventories.Controllers
{
    [Area("Accounts")]
    public class ExpenditureAllocationController : BaseController
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpenditureAllocation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.ExpenditureAllocation);

            return View(new SearchListViewModel()
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.ExpenditureAllocation.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.ChartOfAccount,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }
        
        public JsonResult GetAccountTransactionsDetails(long headID)
        {
            var transaction = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAccountTransactionHeadById(headID);
            var detailViewModelList = new List<ExpenditureAllocationTransactionsViewModel>();

            if (transaction != null && transaction.AccountTransactionDetails.Count()>0)
            {
                foreach (var detailDTOItem in transaction.AccountTransactionDetails)
                {
                    var detailViewModel = new ExpenditureAllocationTransactionsViewModel();

                    detailViewModel.Amount = detailDTOItem.Amount;
                    detailViewModel.AccountID = detailDTOItem.AccountID;
                    detailViewModel.AccountTransactionDetailIID = detailDTOItem.AccountTransactionDetailIID;
                    detailViewModel.AccountTransactionHeadID = transaction.AccountTransactionHeadIID;
                    detailViewModel.CostCenterID = detailDTOItem.CostCenterID;
                    detailViewModel.UpdatedBy = detailDTOItem.UpdatedBy;
                    detailViewModel.UpdatedDate = detailDTOItem.UpdatedDate;
                    detailViewModel.InvoiceAmount = Convert.ToDouble(detailDTOItem.InvoiceAmount);
                    detailViewModel.CreatedDate = detailDTOItem.CreatedDate;
                    detailViewModel.InvoiceNumber = detailDTOItem.InvoiceNumber;
                    detailViewModel.PaidAmount = Convert.ToDouble(detailDTOItem.PaidAmount);
                    //detailViewModel.DueDate = detailDTOItem.PaymentDue;
                    detailViewModel.ReferenceNumber = detailDTOItem.ReferenceNumber;
                    detailViewModel.Remarks = detailDTOItem.Remarks;
                    detailViewModel.ReturnAmount = Convert.ToDouble(detailDTOItem.ReturnAmount);
                    detailViewModel.ReceivableID = detailDTOItem.ReceivableID;
                    detailViewModel.SubLedgerID = detailDTOItem.SubLedgerID;

                    if (detailDTOItem.CostCenter != null)
                        detailViewModel.CostCenter = new KeyValueViewModel() { Key = detailDTOItem.CostCenter.Key, Value = detailDTOItem.CostCenter.Value };

                    if (detailDTOItem.Account != null)
                        detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };

                    if (detailDTOItem.AccountGroup != null)
                        detailViewModel.AccountGroup = new KeyValueViewModel() { Key = detailDTOItem.AccountGroup.Key, Value = detailDTOItem.AccountGroup.Value };

                    if (detailDTOItem.SubLedger != null)
                        detailViewModel.AccountSubLedgers = new KeyValueViewModel() { Key = detailDTOItem.SubLedger.Key, Value = detailDTOItem.SubLedger.Value };


                    if (detailDTOItem.Budget != null)
                        detailViewModel.Budget = new KeyValueViewModel() { Key = detailDTOItem.Budget.Key, Value = detailDTOItem.Budget.Value };
                    //if (detailDTOItem.AccountTypeID != null)
                    //{
                    //    if (detailDTOItem.AccountTypeID == 1)
                    //    {
                    //        detailViewModel.AccountTypes = new KeyValueViewModel() { Key = "1", Value = "ChartOfAccounts" };
                    //    }
                    //    else
                    //    {
                    //        detailViewModel.AccountTypes = new KeyValueViewModel() { Key = "2", Value = "VendorCustomerAccounts" };
                    //    }
                    //}
                    //else
                    //{
                    //    if(detailDTOItem.Account != null)
                    //    {
                    //        detailViewModel.AccountTypes = new KeyValueViewModel() { Key = "1", Value = "ChartOfAccounts" };
                    //    }
                    //}

                    if (detailViewModel.Amount > 0)
                    {
                        detailViewModel.Debit = detailViewModel.Amount;
                        detailViewModel.DebitTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);

                    }
                    else
                    {
                        detailViewModel.Credit = -1 * detailViewModel.Amount;
                        detailViewModel.CreditTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);
                    }



                    detailViewModelList.Add(detailViewModel);
                }
            }
            return Json(detailViewModelList);
        }
    }
}
