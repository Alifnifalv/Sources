using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using common = Eduegate.Services.Contracts.Commons;
using System.Data;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AssetEntryController : BaseSearchController
    {

        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

        // GET: Inventories/InventoryDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.AssetEntry);
            return View(new SearchListViewModel
            {
                //ControllerName = Eduegate.Eduegate.Infrastructure.Enums.SearchView.AssetEntry.ToString(),
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.AssetEntry.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AssetEntry,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                IsEditableLink = true,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Average Quantity</div>
                                            <div class='status-label-color'><label class='status-color-label orange'></label>More Quantity</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>Less Quantity</div>
                                        </div>
                                    </li>",
                IsChild = false,
                HasChild = true,
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.AssetEntryTransactionDetails);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AssetEntryTransactionDetails,
                HeaderList = metadata.Columns,
                //SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("HeadIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.AssetEntry,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.AssetEntry;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.AssetEntryTransactionDetails;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1492;
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.AssetEntrySummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var vm = new AssetEntryViewModel()
            {
                DetailViewModel = new List<AssetEntryDetailViewModel>() { new AssetEntryDetailViewModel() { } },
                MasterViewModel = new AssetEntryMasterViewModel()
                {
                    EntryDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "36", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    // DocumentType = new KeyValueViewModel() { Key = "1400", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.AssetEntry)},
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) }
                }
            };

            return Json(vm);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }
        [HttpGet]
        public ActionResult GetAccountByID(long ID = 0)
        {
            var account = ClientFactory.AccountingServiceClient(CallContext).GetAccount(ID);
            return Json(account);
        }


        [HttpGet]
        public ActionResult GetAssetByID(long ID = 0)
        {
            AssetDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetById(ID);
            return Json(dto);
        }
        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var assetEntryViewModel = JsonConvert.DeserializeObject<AssetEntryViewModel>(jsonData.SelectToken("data").ToString());

            try
            {
                if (assetEntryViewModel != null)
                {
                    if (assetEntryViewModel.DetailViewModel != null)
                    {
                        assetEntryViewModel.DetailViewModel = assetEntryViewModel.DetailViewModel.Where(x => x.AssetCode != null).ToList();

                        if (assetEntryViewModel.DetailViewModel.Any(x => x.AssetGlAccount == null))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select Asset or account" });
                        }
                        if (assetEntryViewModel.DetailViewModel.Any(x => x.AssetCode != null && x.Quantity == 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Quantity need to be entered for Asset" });
                        }
                        if (assetEntryViewModel.DetailViewModel.Any(x => (x.Credit == 0 || x.Credit == null) && (x.Debit == 0 || x.Debit == null)))
                        {
                            return Json(new { IsError = true, UserMessage = "Please enter Debit or Credit amount" });
                        }
                        if (assetEntryViewModel.DetailViewModel.Any(x => x.AssetCode != null && x.StartDate == null))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select Asset Start date" });
                        }
                        if (assetEntryViewModel.DetailViewModel.Sum(x => x.Credit * (x.Quantity == 0 ? 1 : x.Quantity)) != assetEntryViewModel.DetailViewModel.Sum(x => x.Debit * (x.Quantity == 0 ? 1 : x.Quantity)))
                        {
                            return Json(new { IsError = true, UserMessage = "Credit Amount and Debit amount should be equal. Consider Asset Value * Quatity" });
                        }

                        if (assetEntryViewModel.DetailViewModel.Any(x => x.Credit > 0 && x.Debit > 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Both credit and Debit cannot be allowed for an item" });
                        }

                        if (assetEntryViewModel.DetailViewModel.Any(x => x.AssetCode != null && x.AssetGlAccount != null && x.AssetGlAccID != Convert.ToInt64(x.AssetGlAccount.Key)))
                        {
                            return Json(new { IsError = true, UserMessage = "Asset Code's GL Account cannot be changed" });
                        }
                        if (assetEntryViewModel.DetailViewModel.Where(x => x.AssetCode == null).Count() == assetEntryViewModel.DetailViewModel.Count())
                        {
                            return Json(new { IsError = true, UserMessage = "Atleaset one asset code should be there" });
                        }

                    }
                    foreach (var detailItem in assetEntryViewModel.DetailViewModel.Where(x => x.AssetCode != null).ToList())
                    {
                        if (detailItem.DetailIID == 0)// for new items
                        {
                            AssetDTO AssetDTOInDB = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetById(Convert.ToInt64(detailItem.AssetCode.Key));
                            if (AssetDTOInDB != null && AssetDTOInDB.AssetValue != null)
                            {
                                return Json(new
                                {
                                    IsError = true,
                                    UserMessage = "For Asset " + AssetDTOInDB.AssetCode + "-" + AssetDTOInDB.Description
                                                                                + " there is already an entry with Start Date: " + AssetDTOInDB.StartDate.ToString()
                                                                                + " , Asset Value " + AssetDTOInDB.AssetValue.ToString()
                                                                                + " and Quantity " + AssetDTOInDB.Quantity.ToString()
                                });
                            }
                        }
                    }

                    AssetTransactionHeadDTO HeadDTO = new AssetEntryViewModel().ToAssetTransactionHeadDTO(assetEntryViewModel);

                    AssetTransactionHeadDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).SaveAssetTransaction(HeadDTO);
                    assetEntryViewModel = new AssetEntryViewModel().ToAssetTransactionViewModel(dto);

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetEntryController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }
            return Json(assetEntryViewModel);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AssetTransactionHeadDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetTransactionHeadById(ID);
            AssetEntryViewModel assetEntryViewModel = new AssetEntryViewModel().ToAssetTransactionViewModel(dto);
            return Json(assetEntryViewModel);
        }

        public string GetNextTransactionNumber(long documentTypeID, List<common.KeyValueParameterDTO> parameters = null)
        {
            string nextTransactionNumber = string.Empty;
            var entity = ClientFactory.MetadataServiceClient(CallContext).GetDocumentType(documentTypeID);
            if (entity.IsNotNull())

            {
                var transactionNo = default(string);
                nextTransactionNumber = entity.TransactionNoPrefix;
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        transactionNo += entity.TransactionNoPrefix.Replace("{" + item.ParameterName.Trim().ToUpper() + "}", item.ParameterValue);
                    }
                    nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }
                nextTransactionNumber += entity.LastTransactionNo.IsNull() ? "1" : Convert.ToString(entity.LastTransactionNo + 1);
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }
    }
}
