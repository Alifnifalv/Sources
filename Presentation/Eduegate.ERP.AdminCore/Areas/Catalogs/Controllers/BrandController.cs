using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
    public class BrandController : BaseSearchController
    {
        // GET: Brand
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.Brand);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.Brand.ToString(),
                ViewName = Infrastructure.Enums.SearchView.Brand,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                                        </div>
                                    </li>",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Brand";
            viewModel.ListActionName = "Brand";
            viewModel.ViewModel = new BrandViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BrandStatus", Url = "Mutual/GetLookUpData?lookType=BrandStatus" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ImageTypes", Url = "Mutual/GetLookUpData?lookType=ImageTypes" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BrandTags", Url = "Brand/BrandTags" });
            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("Create", "CRUD");
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.Brand, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.BrandSummary);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var brandService = ClientFactory.BrandServiceClient(this.CallContext);
            var result = brandService.GetBrand(ID);
            return Json(BrandViewModel.FromDTO(result));
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        public JsonResult Save([FromBody] BrandViewModel vm)
        {
            try
            {
                var userID = CallContext.LoginID;
                //var fileName = System.IO.Path.GetFileName(vm.ImageUrl);

                //string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
                //      new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Brands.ToString(), Constants.TEMPFOLDER, userID, fileName);

                //if (System.IO.File.Exists(tempFolderPath))
                //{
                //    string orignalFolderPath = string.Format("{0}//{1}//{2}",
                //           new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Brands.ToString(), fileName);

                //    if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
                //        Directory.CreateDirectory(orignalFolderPath);

                //    System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);

                //    vm.Logo = fileName;
                //}

                //foreach (var mapVm in vm.ImageMap.Maps)
                //{
                //    fileName = System.IO.Path.GetFileName(mapVm.ImageUrl);
                //    //move from temparary folder to oringal location
                //    tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
                //            new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Temporary.ToString(), Constants.TEMPFOLDER, userID, fileName);

                //    if (System.IO.File.Exists(tempFolderPath))
                //    {
                //        string orignalFolderPath = string.Format("{0}//{1}//{2}//{3}",
                //               new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Brands.ToString(), mapVm.ImageType, fileName);

                //        if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
                //            Directory.CreateDirectory(Path.GetDirectoryName(orignalFolderPath));

                //        System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);

                //        mapVm.ImageName = fileName;
                //    }
                //}

                var vm2 = BrandViewModel.FromDTO(ClientFactory.BrandServiceClient(CallContext).SaveBrand(BrandViewModel.ToDTO(vm)));

                //foreach (var mapVm2 in vm2.ImageMap.Maps)
                //{
                //    mapVm2.ImageUrl = string.Format("{0}//{1}//{2}//{3}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.Brands.ToString(), mapVm2.ImageType, mapVm2.ImageName);
                //}

                return Json(vm2);
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BrandController>.Fatal(ex.Message.ToString(), ex);
                throw;
            }
        }

        public bool BrandNameAvailibility(string brandName, long brandIID)
        {
            return ClientFactory.BrandServiceClient(this.CallContext).BrandNameAvailibility(brandName,brandIID);
        }

        [HttpGet]
        public ActionResult BrandTags()
        {
            try
            {
                var tags = KeyValueViewModel.FromDTO(ClientFactory.BrandServiceClient(this.CallContext).GetBrandTags());
                return Json(tags);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BrandController>.Fatal(ex.Message.ToString(), ex);
                throw;
            }
        }
    }
}