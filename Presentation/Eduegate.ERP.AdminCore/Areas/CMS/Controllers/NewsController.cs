using Newtonsoft.Json;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Services;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;

namespace Eduegate.ERP.Admin.Areas.CMS
{
    [Area("CMS")]
    public class NewsController : BaseSearchController
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.News);

            return View(new SearchListViewModel
            {
                ControllerName = Eduegate.Infrastructure.Enums.SearchView.News.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.News,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                UserViews = metadata.UserViews,
                SortColumns = metadata.SortColumns,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                            <div class='right status-label'>
                //                                <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                //                                <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                            </div>
                //                        </li>"
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.News, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.NewsSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "News";
            viewModel.ListActionName = "News";
            viewModel.ViewModel = new NewsViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "NewsType", Url = "Mutual/GetLookUpData?lookType=NewsType" });
            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("Create", "CRUD");
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var dto = ClientFactory.NewsDataServiceClient(CallContext).GetNewsByID(ID.ToString());
            var vm = NewsViewModel.FromDTO(dto);
            vm.NewsImage = string.Format("{0}//{1}//{2}",
               new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.News.ToString(), vm.ImageUrl);
            vm.ImageUrl = vm.NewsImage; // assigning newsimage to ImageUrl to show it in image preview
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] NewsViewModel vm)
        {
            var userID = CallContext.LoginID;
            var fileName = System.IO.Path.GetFileName(vm.ImageUrl);
            //move from temparary folder to oringal location
            string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
                    new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.News.ToString(), Constants.TEMPFOLDER, userID, fileName);

            if (System.IO.File.Exists(tempFolderPath))
            {
                string orignalFolderPath = string.Format("{0}//{1}//{2}",
                       new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.News.ToString(), fileName);

                if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
                    Directory.CreateDirectory(orignalFolderPath);

                System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);

                vm.ImageUrl = fileName;
            }

            var result = ServiceHelper.HttpPostRequest(ServiceHost + Eduegate.Framework.Helper.Constants.NEWS_DATA_SERVICE + "SaveNews", NewsViewModel.ToDTO(vm), this.CallContext);
            var updatedDTO = JsonConvert.DeserializeObject<NewsDTO>(result);
            var updatedVM = NewsViewModel.FromDTO(updatedDTO);
            updatedVM.NewsImage = string.Format("{0}\\{1}\\{2}",
                  new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.News.ToString(), vm.ImageUrl);
            updatedVM.ImageUrl = updatedVM.NewsImage;
            return Json(updatedVM);
        }
    }
}