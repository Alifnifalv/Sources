using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Search;
using Eduegate.Framework.Services;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using System.Linq;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework.Contracts.Common;
//using Eduegate.Framework.Enums;

using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using Eduegate.Framework.Enums;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AssetMasterController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

         public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.AssetMaster);

            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.AssetMaster.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AssetMaster,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                UserViews = metadata.UserViews,
                SortColumns = metadata.SortColumns,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            var view = Eduegate.Infrastructure.Enums.SearchView.AssetMaster;
            orderBy = orderBy.Equals("null") ? "" : orderBy;

            if (orderBy.Trim().IsNullOrEmpty())
                orderBy = "CreatedDate";

            return base.SearchData(view, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.AssetMasterSummary);
        }       

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var vm = new AssetViewModel()
            {
                DetailViewModel = new List<AssetDetailViewModel>() { new AssetDetailViewModel() { } },
                MasterViewModel = new AssetMasterViewModel()                
            };

            return Json(vm);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AssetDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetById(ID);
            AssetDetailViewModel assetDetailViewModel = new AssetViewModel().ToViewModel(dto);
            AssetViewModel assetViewModel =  new AssetViewModel()
            {
                DetailViewModel = new List<AssetDetailViewModel>() { assetDetailViewModel },
                MasterViewModel = new AssetMasterViewModel() { AssetIID= ID }
            };
            
            return Json(assetViewModel);
        }        

        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var assetViewModel = JsonConvert.DeserializeObject<AssetViewModel>(jsonData.SelectToken("data").ToString());

            try
            {
                var fixedAssetServiceClient = ClientFactory.FixedAssetServiceClient(CallContext);
                if (assetViewModel != null )
                {
                    if (assetViewModel.DetailViewModel.Where(x => x.AssetCode == null ).Count()>0)
                    {
                        return Json(new { IsError = true, UserMessage = "Asset code should be entered" });
                    }
                    if (assetViewModel.DetailViewModel.Where(x => x.Description == null).Count() > 0)
                    {
                        return Json(new { IsError = true, UserMessage = "Asset Description should be entered" });
                    }
                    if (assetViewModel.DetailViewModel.Where(x => x.AssetCategory == null).Count() > 0)
                    {
                        return Json(new { IsError = true, UserMessage = "Asset Category should be entered" });
                    }
                    if (assetViewModel.DetailViewModel.Where(x => x.AssetGlAccount == null).Count() > 0)
                    {
                        return Json(new { IsError = true, UserMessage = "Asset GL Account should be entered" });
                    }
                    if (assetViewModel.DetailViewModel.Where(x => x.DepreciationExpGLAccount == null).Count() > 0)
                    {
                        return Json(new { IsError = true, UserMessage = "Asset Depreciation GL Account should be entered" });
                    }
                    if (assetViewModel.DetailViewModel.Where(x => x.AccumulatedDepGLAccount == null).Count() > 0)
                    {
                        return Json(new { IsError = true, UserMessage = "Asset Accumlated Depreciation GL Account should be entered" });
                    }
                    if (assetViewModel.DetailViewModel.Where(x => x.DepreciationYears == 0).Count() > 0)
                    {
                        return Json(new { IsError = true, UserMessage = "DepreciationYears should be entered" });
                    }

                    foreach (var detailVM in assetViewModel.DetailViewModel)
                    {
                        if (assetViewModel.DetailViewModel.Where(x => x.AssetCode == detailVM.AssetCode).Count()> 1)
                        {
                            return Json(new { IsError = true, UserMessage = "Asset code should be unique. Asset code "+ detailVM.AssetCode+ " repeated in the screen list." });
                        }
                        var assetByAssetCode = fixedAssetServiceClient.GetAssetByAssetCode(detailVM.AssetCode);
                        if(assetByAssetCode!=null && assetByAssetCode.AssetIID != detailVM.AssetIID)
                        {
                            return Json(new { IsError = true, UserMessage = "Asset code should be unique. Asset code " + detailVM.AssetCode + " already available in Database." });
                        }
                    }
                    //Delete Record - In Edit mode MasterViewModel is set with the Editing Asset's ID ; Only one asset should edited at a time
                    long AssetIDTobeDleted = assetViewModel.MasterViewModel.AssetIID;
                    AssetDetailViewModel assetDetailViewModel = assetViewModel.DetailViewModel.Where(x => x.AssetIID == AssetIDTobeDleted).FirstOrDefault();
                    if(assetDetailViewModel == null && AssetIDTobeDleted >0)
                    {
                        // This ID need to be delted from DB
                       bool blnDeleteStatus= fixedAssetServiceClient.DeleteAsset(AssetIDTobeDleted);
                    }                    

                    List<AssetDTO> assetDTOList = assetViewModel.ToAssetDTO(assetViewModel.DetailViewModel);
                    assetDTOList = fixedAssetServiceClient.SaveAssets(assetDTOList);

                    if(assetDTOList.Where(x=>x.AssetIID ==0 ).Count()> 0)
                    {
                        return Json(new { IsError = true, UserMessage = "Failed to save assets" });
                    }

                    var dto = assetDTOList.FirstOrDefault();// After save show the first item in the edit mode. This screen supports only one asset to be edited at a time.
                    if (dto != null)
                    {
                        assetViewModel = new AssetViewModel();
                        AssetDetailViewModel returnedassetDetailViewModel = new AssetViewModel().ToViewModel(dto);
                        assetViewModel.DetailViewModel = new List<AssetDetailViewModel>();
                        assetViewModel.DetailViewModel.Add(returnedassetDetailViewModel);
                        assetViewModel.MasterViewModel = new AssetMasterViewModel() { AssetIID = dto.AssetIID };
                    }
                   

                }
                   
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetMasterController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }

           return Json(assetViewModel);
        }

        [HttpGet]
        public JsonResult AssetCodesSearch(string lookupName, string searchText)
        {
           List<KeyValueViewModel> assets = new List<KeyValueViewModel>();
           try
            {
                var DTOList= ClientFactory.FixedAssetServiceClient(CallContext).GetAssetCodesSearch(searchText);
               
                foreach (var dto in DTOList)
                {
                    assets.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                }
                
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetMasterController>.Fatal(exception.Message.ToString(), exception);
            }
            return Json(new { LookUpName = lookupName, Data = assets });
            //return Json(assets);
        }

        [HttpGet]
        public JsonResult AccountCodesSearch(string lookupName, string searchText)
        {
            List<KeyValueViewModel> accounts = new List<KeyValueViewModel>();
            try
            {
                var DTOList = ClientFactory.FixedAssetServiceClient(CallContext).GetAccountCodesSearch(searchText);

                foreach (var dto in DTOList)
                {
                    accounts.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                }

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetMasterController>.Fatal(exception.Message.ToString(), exception);
            }
            return Json(new { LookUpName = lookupName, Data = accounts });
            //return Json(assets);
        }
       
    }
}