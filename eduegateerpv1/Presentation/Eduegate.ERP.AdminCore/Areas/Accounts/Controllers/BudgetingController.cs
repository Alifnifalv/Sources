using Eduegate.Application.Mvc;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Web.Library.Budgeting.Budget;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class BudgetingController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Budgeting(int budgetID = 0)
        {
            var vm = new BudgetingViewModel();

            vm = FillDefaultGridValues();

            if (budgetID == 0)
            {
                vm.IsEdit = false;
            }
            else
            {
                vm.BudgetID = budgetID;
                vm.IsEdit = true;
            }

            return View(vm);
        }

        public BudgetingViewModel FillDefaultGridValues()
        {
            var vm = new BudgetingViewModel();

            vm.Allocations = new List<AllocationAmountViewModel>();
            vm.BudgetingAccountDetail = new BudgetingAccountDetailViewModel();

            DateTime fromDate = new DateTime(2024, 1, 1);
            DateTime endDate = new DateTime(2024, 12, 31);

            while (fromDate <= endDate)
            {
                var title = fromDate.ToString("MMM") + " - " + fromDate.Year;
                vm.HeaderTitles.Add(title);

                vm.Allocations.Add(new AllocationAmountViewModel()
                {
                    MonthName = fromDate.ToString("MMMM"),
                    MonthID = fromDate.Month,
                    Amount = null,
                    Year = fromDate.Year,
                });

                if (fromDate.Month == 12) // If the current month is December, move to the next year
                {
                    fromDate = new DateTime(fromDate.Year + 1, 1, 1);
                }
                else
                {
                    fromDate = fromDate.AddMonths(1);
                }
            }

            return vm;
        }

        [HttpPost]
        public ActionResult FillBudgetingGridByMaster([FromBody] BudgetingViewModel vm)
        {
            var budgetList = new List<BudgetingViewModel>();

            var headerTitles = new List<string>();
            var allocations = new List<AllocationAmountViewModel>();

            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var fromDate = string.IsNullOrEmpty(vm.FromDateString) ? (DateTime?)null : DateTime.ParseExact(vm.FromDateString, dateFormat, CultureInfo.InvariantCulture);
            var endDate = string.IsNullOrEmpty(vm.ToDateString) ? (DateTime?)null : DateTime.ParseExact(vm.ToDateString, dateFormat, CultureInfo.InvariantCulture);

            while (fromDate <= endDate)
            {
                var title = fromDate.Value.ToString("MMM") + " - " + fromDate.Value.Year;

                headerTitles.Add(title);

                allocations.Add(new AllocationAmountViewModel()
                {
                    MonthName = fromDate.Value.ToString("MMMM"),
                    MonthID = fromDate.Value.Month,
                    Amount = null,
                    Year = fromDate.Value.Year,
                });

                if (fromDate.Value.Month == 12) // If the current month is December, move to the next year
                {
                    fromDate = new DateTime(fromDate.Value.Year + 1, 1, 1);
                }
                else
                {
                    fromDate = fromDate.Value.AddMonths(1);
                }
            }

            foreach (var group in vm.AccountGroupList)
            {
                var groupID = int.Parse(group.Key);
                var accountList = ClientFactory.SchoolServiceClient(CallContext).GetAccountByGroupID(groupID);

                var groupData = ClientFactory.SchoolServiceClient(CallContext).GetAccountGroupDataByID(groupID);

                foreach (var account in accountList)
                {
                    var accountDetail = new BudgetingAccountDetailViewModel()
                    {
                        AccountGroup = new KeyValueViewModel()
                        {
                            Key = group.Key,
                            Value = group.Value
                        },
                        Account = new KeyValueViewModel()
                        {
                            Key = account.Key,
                            Value = account.Value
                        },
                        GroupDefaultSide = groupData.Default_Side
                    };

                    budgetList.Add(new BudgetingViewModel()
                    {
                        BudgetEntryIID = 0,
                        BudgetingAccountDetail = accountDetail,
                        Allocations = allocations,
                        HeaderTitles = headerTitles,
                    });
                }
            }

            var returnVM = new BudgetingViewModel()
            {
                BudgetListModel = budgetList,
                HeaderTitles = headerTitles,
            };

            return Json(new { IsError = false, Response = returnVM });
        }

        [HttpPost]
        public ActionResult SaveBudgettingEntries([FromBody] List<BudgetingViewModel> budgetEntiesList)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var bugetEntryDTOs = new List<BudgetEntryDTO>();

            foreach (var budgetEntry in budgetEntiesList)
            {
                var allocationDtos = new List<BudgetEntryAllocationDTO>();
                foreach (var alloc in budgetEntry.Allocations)
                {
                    // Get the first day of the month
                    DateTime firstDayOfMonth = new DateTime(alloc.Year.Value, alloc.MonthID.Value, 1);

                    // Get the last day of the month
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    allocationDtos.Add(new BudgetEntryAllocationDTO()
                    {
                        BudgetEntryAllocationIID = alloc.BudgetEntryAllocationIID,
                        BudgetEntryID = budgetEntry.BudgetEntryIID,
                        Amount = alloc.Amount,
                        StartDate = firstDayOfMonth,
                        EndDate = lastDayOfMonth,
                        SuggestedValue = alloc.SuggestedValue,
                        Percentage = alloc.Percentage,
                        EstimateValue = alloc.EstimateValue,
                        Remarks = alloc.Remarks,
                    });
                }

                var accountDTOs = new List<BudgetEntryAccountMapDTO>
                {
                    new BudgetEntryAccountMapDTO()
                    {
                        BudgetEntryAccountMapIID = budgetEntry.BudgetingAccountDetail.BudgetEntryAccountMapIID,
                        GroupID = budgetEntry.BudgetingAccountDetail.AccountGroup == null || string.IsNullOrEmpty(budgetEntry.BudgetingAccountDetail.AccountGroup.Key) ? (int?)null : int.Parse(budgetEntry.BudgetingAccountDetail.AccountGroup.Key),
                        AccountID = budgetEntry.BudgetingAccountDetail.Account == null || string.IsNullOrEmpty(budgetEntry.BudgetingAccountDetail.Account.Key) ? (int?)null : int.Parse(budgetEntry.BudgetingAccountDetail.Account.Key),
                        Remarks = budgetEntry.BudgetingAccountDetail.Remarks,
                    }
                };

                var costCenterDTOs = new List<BudgetEntryCostCenterMapDTO>
                {
                    new BudgetEntryCostCenterMapDTO()
                    {
                        BudgetEntryID = budgetEntry.BudgetEntryIID,
                        CostCenterID = budgetEntry.CostCenter == null || string.IsNullOrEmpty(budgetEntry.CostCenter.Key) ? (int?)null : int.Parse(budgetEntry.CostCenter.Key),
                    }
                };

                bugetEntryDTOs.Add(new BudgetEntryDTO()
                {
                    BudgetEntryIID = budgetEntry.BudgetEntryIID,
                    BudgetID = budgetEntry.Budget == null || string.IsNullOrEmpty(budgetEntry.Budget.Key) ? (int?)null : int.Parse(budgetEntry.Budget.Key),
                    BudgetTypeID = budgetEntry.BudgetType == null || string.IsNullOrEmpty(budgetEntry.BudgetType.Key) ? (byte?)null : byte.Parse(budgetEntry.BudgetType.Key),
                    BudgetSuggestionID = budgetEntry.BudgetSuggestion == null || string.IsNullOrEmpty(budgetEntry.BudgetSuggestion.Key) ? (byte?)null : byte.Parse(budgetEntry.BudgetSuggestion.Key),
                    SuggestedStartDate = string.IsNullOrEmpty(budgetEntry.FromDateString) ? (DateTime?)null : DateTime.ParseExact(budgetEntry.FromDateString, dateFormat, CultureInfo.InvariantCulture),
                    SuggestedEndDate = string.IsNullOrEmpty(budgetEntry.ToDateString) ? (DateTime?)null : DateTime.ParseExact(budgetEntry.ToDateString, dateFormat, CultureInfo.InvariantCulture),
                    SuggestedValue = budgetEntry.SuggestedValue,
                    Percentage = budgetEntry.Percentage,
                    EstimateValue = budgetEntry.EstimateValue,
                    Amount = budgetEntry.Amount,
                    BudgetEntryAllocationDtos = allocationDtos,
                    BudgetEntryAccountMapDtos = accountDTOs,
                    BudgetEntryCostCenterDtos = costCenterDTOs,
                });
            }

            try
            {
                var result = ClientFactory.AccountingServiceClient(CallContext).SaveBudgetEntry(bugetEntryDTOs);

                if (result != null)
                {
                    return Json(new { IsError = false, Response = "Saved successfully!" });
                }
                else
                {
                    return Json(new { IsError = true, Response = "Saving failed!" });
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Budgeting entry saving failed. Error message: {errorMessage}", ex);

                return Json(new { IsError = true, Response = "Saving failed!" });
            }
        }

        [HttpGet]
        public JsonResult GetBudgetDetailsByID(int budgetID)
        {
            var result = ClientFactory.AccountingServiceClient(CallContext).GetBudgetDetailsByID(budgetID);

            return Json(result);
        }

        [HttpGet]
        public JsonResult FillBudgetEntriesByID(int budgetID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var budgetEntries = ClientFactory.AccountingServiceClient(CallContext).FillBudgetEntriesByID(budgetID);

            var budgetList = new List<BudgetingViewModel>();
            var returnVM = new BudgetingViewModel();
            var headerTitles = new List<string>();
            foreach (var budgetEntry in budgetEntries)
            {
                var allocations = new List<AllocationAmountViewModel>();

                var accountMapDTO = budgetEntry.BudgetEntryAccountMapDtos.FirstOrDefault();

                var accountMap = new BudgetingAccountDetailViewModel()
                {
                    BudgetEntryAccountMapIID = accountMapDTO.BudgetEntryAccountMapIID,
                    BudgetEntryID = accountMapDTO.BudgetEntryID,
                    AccountGroup = accountMapDTO.GroupID.HasValue ? new KeyValueViewModel()
                    {
                        Key = accountMapDTO.Group?.Key,
                        Value = accountMapDTO.Group?.Value
                    } : new KeyValueViewModel(),
                    Account = accountMapDTO.AccountID.HasValue ? new KeyValueViewModel()
                    {
                        Key = accountMapDTO.Account?.Key,
                        Value = accountMapDTO.Account?.Value
                    } : new KeyValueViewModel(),
                    Remarks = accountMapDTO.Remarks,
                    GroupDefaultSide=accountMapDTO.GroupDefaultSide

                };

                headerTitles = new List<string>();
                foreach (var allocation in budgetEntry.BudgetEntryAllocationDtos)
                {
                    var fromDate = allocation.StartDate;

                    var title = allocation.StartDate.Value.ToString("MMM") + " - " + allocation.StartDate.Value.Year;
                    headerTitles.Add(title);

                    allocations.Add(new AllocationAmountViewModel()
                    {
                        BudgetEntryAllocationIID = allocation.BudgetEntryAllocationIID,
                        Amount = allocation.Amount,
                        StartDateString = allocation.StartDate.HasValue ? allocation.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        EndDateString = allocation.EndDate.HasValue ? allocation.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        SuggestedStartDateString = allocation.SuggestedStartDate.HasValue ? allocation.SuggestedStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        SuggestedEndDateString = allocation.SuggestedEndDate.HasValue ? allocation.SuggestedEndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        SuggestedValue = allocation.SuggestedValue,
                        Percentage = allocation.Percentage,
                        EstimateValue = allocation.EstimateValue,
                        MonthID = fromDate.HasValue ? fromDate.Value.Month : (int?)null,
                        MonthName = fromDate.HasValue ? fromDate.Value.ToString("MMMM") : null,
                        Year = fromDate.HasValue ? fromDate.Value.Year : null,
                        Remarks = allocation.Remarks,
                    });
                }

                budgetList.Add(new BudgetingViewModel()
                {
                    BudgetEntryIID = budgetEntry.BudgetEntryIID,
                    BudgetID = budgetEntry.BudgetID,
                    SuggestedValue = budgetEntry.SuggestedValue,
                    Percentage = budgetEntry.Percentage,
                    EstimateValue = budgetEntry.EstimateValue,
                    Amount = budgetEntry.Amount,
                    HeaderTitles = headerTitles,
                    BudgetingAccountDetail = accountMap,
                    Allocations = allocations,
                });
            }

            returnVM = new BudgetingViewModel()
            {
                BudgetListModel = budgetList,
                HeaderTitles = headerTitles,
            };

            return Json(new { IsError = false, Response = returnVM });
        }

        [HttpPost]
        public ActionResult GetBudgetSuggestionValue([FromBody] BudgetingViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var returnVM = new BudgetingViewModel();
            var budgetList = new List<BudgetingViewModel>();
            var headerTitles = new List<string>();

            byte? suggestionType = byte.Parse(vm.BudgetSuggestion.Key);
            int? budgetID = byte.Parse(vm.Budget.Key);

            var accountMapListDTO = new List<BudgetEntryAccountMapDTO>();
            var accountGroupIDs = new List<long?>();
            var accountIDs = new List<long?>();

            //foreach (var grp in vm.AccountGroupList)
            //{
            //    var groupID = int.Parse(grp.Key);
            //    accountGroupIDs.Add(groupID);

            //    var accountList = ClientFactory.SchoolServiceClient(CallContext).GetAccountByGroupID(groupID);

            //    foreach (var account in accountList)
            //    {
            //        if (!string.IsNullOrEmpty(account.Key))
            //        {
            //            accountMapListDTO.Add(new BudgetEntryAccountMapDTO
            //            {
            //                Group = new KeyValueDTO() { Key = grp.Key, Value = grp.Value },
            //                Account = new KeyValueDTO() { Key = account.Key, Value = account.Value },
            //            });
            //        }
            //    }
            //}

            foreach (var budgetAcnt in vm.BudgetingAccountDetail.AccountListModel)
            {
                var groupID = int.Parse(budgetAcnt.AccountGroup.Key);
                accountGroupIDs.Add(groupID);                

                if (!string.IsNullOrEmpty(budgetAcnt.Account.Key))
                {
                    var accountID = long.Parse(budgetAcnt.Account.Key);
                    accountIDs.Add(accountID);

                    accountMapListDTO.Add(new BudgetEntryAccountMapDTO
                    {
                        Group = new KeyValueDTO() { Key = budgetAcnt.AccountGroup.Key, Value = budgetAcnt.AccountGroup.Value },
                        Account = new KeyValueDTO() { Key = budgetAcnt.Account.Key, Value = budgetAcnt.Account.Value },
                    });
                }
            }

            DateTime? fromDate = string.IsNullOrEmpty(vm.FromDateString) ? (DateTime?)null : DateTime.ParseExact(vm.FromDateString, dateFormat, CultureInfo.InvariantCulture);
            DateTime? toDate = string.IsNullOrEmpty(vm.ToDateString) ? (DateTime?)null : DateTime.ParseExact(vm.ToDateString, dateFormat, CultureInfo.InvariantCulture);

            DateTime? suggestedFromDate = string.IsNullOrEmpty(vm.SuggestedFromDateString) ? (DateTime?)null : DateTime.ParseExact(vm.SuggestedFromDateString, dateFormat, CultureInfo.InvariantCulture);
            DateTime? suggestedToDate = string.IsNullOrEmpty(vm.SuggestedToDateString) ? (DateTime?)null : DateTime.ParseExact(vm.SuggestedToDateString, dateFormat, CultureInfo.InvariantCulture);

            var suggestionList = ClientFactory.AccountingServiceClient(CallContext).GetBudgetSuggestionValue(budgetID, suggestionType, accountGroupIDs, suggestedFromDate, suggestedToDate, accountIDs);

            var allocations = new List<AllocationAmountViewModel>();
            while (fromDate <= toDate)
            {
                var title = fromDate.Value.ToString("MMM") + " - " + fromDate.Value.Year;

                headerTitles.Add(title);

                // Get the first day of the month
                DateTime? firstDayOfMonth = new DateTime(fromDate.Value.Year, fromDate.Value.Month, 1);

                // Get the last day of the month
                DateTime? lastDayOfMonth = firstDayOfMonth.HasValue ? firstDayOfMonth.Value.AddMonths(1).AddDays(-1) : (DateTime?)null;

                allocations.Add(new AllocationAmountViewModel()
                {
                    MonthName = fromDate.Value.ToString("MMMM"),
                    MonthID = fromDate.Value.Month,
                    Amount = null,
                    Year = fromDate.Value.Year,
                    StartDateString = firstDayOfMonth.HasValue ? firstDayOfMonth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    EndDateString = lastDayOfMonth.HasValue ? lastDayOfMonth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                });

                if (fromDate.Value.Month == 12) // If the current month is December, move to the next year
                {
                    fromDate = new DateTime(fromDate.Value.Year + 1, 1, 1);
                }
                else
                {
                    fromDate = fromDate.Value.AddMonths(1);
                }
            }

            var groupedSuggestionList = suggestionList.GroupBy(x => x.AccountID).ToList();

            var accountMapDetailModel = new BudgetingAccountDetailViewModel();

            var allocationModels = allocations.Select(a => new AllocationAmountViewModel
            {
                MonthID = a.MonthID,
                MonthName = a.MonthName,
                Amount = a.Amount,
                Year = a.Year,
                SuggestedValue = a.SuggestedValue,
                StartDateString = a.StartDateString,
                EndDateString = a.EndDateString,
                SuggestedStartDateString = a.SuggestedStartDateString,
                SuggestedEndDateString = a.SuggestedEndDateString,
                Remarks = a.Remarks,
            }).ToList();

            decimal? totalAmount = 0;
            foreach (var group in groupedSuggestionList)
            {
                totalAmount = group.ToList().Sum(x => x.Amount);
                accountMapDetailModel = new BudgetingAccountDetailViewModel();

                var entryAllocations = allocations.Select(a => new AllocationAmountViewModel
                {
                    MonthID = a.MonthID,
                    MonthName = a.MonthName,
                    Amount = a.Amount,
                    Year = a.Year,
                    SuggestedValue = a.SuggestedValue,
                    StartDateString = a.StartDateString,
                    EndDateString = a.EndDateString,
                    SuggestedStartDateString = a.SuggestedStartDateString,
                    SuggestedEndDateString = a.SuggestedEndDateString,
                    Remarks = a.Remarks,
                }).ToList();

                // Extract the decimal parts of each value and sum them
                decimal remainder = group.Select(x => x.Amount.Value - Math.Floor(x.Amount.Value)).Sum();

                foreach (var item in group.ToList())
                {
                    accountMapListDTO.RemoveAll(a => a.Account.Key == item.Account.Key);

                    var allocationDet = entryAllocations.FirstOrDefault(x => x.MonthID == item.MonthID);
                    if (allocationDet != null)
                    {
                        // Get the first day of the month
                        DateTime? firstDayOfMonth = new DateTime(item.Year.Value, item.MonthID.Value, 1);

                        // Get the last day of the month
                        DateTime? lastDayOfMonth = firstDayOfMonth.HasValue ? firstDayOfMonth.Value.AddMonths(1).AddDays(-1) : (DateTime?)null;

                        allocationDet.Amount = (decimal)Math.Floor(item.Amount.Value);
                        allocationDet.SuggestedValue = (decimal)Math.Floor(item.Amount.Value);
                        //allocationDet.SuggestedStartDateString = item.StartDate.HasValue ? item.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        //allocationDet.SuggestedEndDateString = item.EndDate.HasValue ? item.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        allocationDet.SuggestedStartDateString = firstDayOfMonth.HasValue ? firstDayOfMonth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        allocationDet.SuggestedEndDateString = lastDayOfMonth.HasValue ? lastDayOfMonth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

                        // Adjust amount for the last item by adding the remainder
                        if (item == group.Last())
                        {
                            allocationDet.Amount += remainder;
                            allocationDet.SuggestedValue += remainder;
                        }
                    }

                    accountMapDetailModel = new BudgetingAccountDetailViewModel()
                    {
                        AccountGroup = item.GroupID.HasValue ? new KeyValueViewModel()
                        {
                            Key = item.Group?.Key,
                            Value = item.Group?.Value
                        } : new KeyValueViewModel(),
                        Account = item.AccountID.HasValue ? new KeyValueViewModel()
                        {
                            Key = item.Account?.Key,
                            Value = item.Account?.Value
                        } : new KeyValueViewModel(),
                        GroupDefaultSide = item.GroupDefaultSide,
                    };
                }

                budgetList.Add(new BudgetingViewModel()
                {
                    BudgetEntryIID = 0,
                    BudgetID = string.IsNullOrEmpty(vm.Budget.Key) ? (int?)null : int.Parse(vm.Budget.Key),
                    SuggestedValue = totalAmount,
                    Amount = totalAmount,
                    HeaderTitles = headerTitles,
                    BudgetingAccountDetail = accountMapDetailModel,
                    Allocations = entryAllocations,
                });
            }

            if (accountMapListDTO != null || accountMapListDTO.Count > 0)
            {
                foreach (var accountMap in accountMapListDTO)
                {
                    totalAmount = 0;
                    accountMapDetailModel = new BudgetingAccountDetailViewModel()
                    {
                        AccountGroup = accountMap.Group != null ? new KeyValueViewModel()
                        {
                            Key = accountMap?.Group?.Key,
                            Value = accountMap?.Group?.Value
                        } : new KeyValueViewModel(),
                        Account = accountMap != null ? new KeyValueViewModel()
                        {
                            Key = accountMap?.Account?.Key,
                            Value = accountMap?.Account?.Value
                        } : new KeyValueViewModel(),
                    };

                    budgetList.Add(new BudgetingViewModel()
                    {
                        BudgetEntryIID = 0,
                        BudgetID = string.IsNullOrEmpty(vm.Budget.Key) ? (int?)null : int.Parse(vm.Budget.Key),
                        SuggestedValue = totalAmount,
                        //Amount = totalAmount,
                        HeaderTitles = headerTitles,
                        BudgetingAccountDetail = accountMapDetailModel,
                        Allocations = allocationModels,
                    });
                }
            }

            returnVM = new BudgetingViewModel()
            {
                BudgetListModel = budgetList,
                HeaderTitles = headerTitles,
            };

            return Json(new { IsError = false, Response = returnVM });
        }

    }
}