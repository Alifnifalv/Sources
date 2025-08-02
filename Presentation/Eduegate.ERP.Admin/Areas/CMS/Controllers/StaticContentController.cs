using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.CMS
{
    public class StaticContentController : BaseSearchController
    {
        // GET: StaticPage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.StaticContent);

            return View(new SearchListViewModel
            {
                ControllerName = Eduegate.Infrastructure.Enums.SearchView.StaticContent.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.StaticContent,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
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
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "StaticContent";
            viewModel.DisplayName = "Static Content Maintenance";
            viewModel.ListButtonDisplayName = "Static Content List";
            viewModel.ListActionName = "StaticContent";
            viewModel.ViewModel = new StaticContentViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "StaticContentType", Url = "Mutual/GetLookUpData?lookType=StaticContentType" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Brands", Url = "Mutual/GetLookUpData?lookType=Brands" });
            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD", new { area = "Frameworks" });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.StaticContent, currentPage, orderBy);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.StaticContentSummary);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID});
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var dto =ClientFactory.StaticContentServiceClient(CallContext).GetStaticContent(ID);
            return Json(StaticContentViewModel.FromDTO(dto), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(StaticContentViewModel vm)
        {
            var userID = CallContext.LoginID;
            var fileName = System.IO.Path.GetFileName(vm.ImageUrl);
            //move from temparary folder to oringal location
            string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
                    ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Styled.ToString(), Constants.TEMPFOLDER, userID, fileName);

            if (System.IO.File.Exists(tempFolderPath))
            {
                string orignalFolderPath = string.Format("{0}//{1}//{2}",
                       ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Styled.ToString(), fileName);

                if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
                    Directory.CreateDirectory(orignalFolderPath);

                System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);

                vm.ImageUrl = fileName;
            }

            var returnDTO = ClientFactory.StaticContentServiceClient(CallContext).SaveStaticContent(StaticContentViewModel.ToDTO(vm));
            var updatedVM = StaticContentViewModel.FromDTO(returnDTO);
            updatedVM.ImageFile = string.Format("{0}\\{1}\\{2}",
                  ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.Styled.ToString(), vm.ImageUrl);
            return Json(updatedVM, JsonRequestBehavior.AllowGet);
        }
    }
}