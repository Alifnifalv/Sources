using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Eduegate.ERP.Admin.Controllers;

using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.CMS
{
    public class BannerController : BaseSearchController
    {
        // GET: Price
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Banner);

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Banner,
                ControllerName = "Banner",
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>"
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Banner, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.BannerSummary);
        }

        //[HttpGet]
        //public ActionResult Create(long ID = 0)
        //{
        //    var viewModel = new CRUDViewModel();
        //    viewModel.Name = "Banner";
        //    viewModel.ListActionName = "Banner";
        //    viewModel.ViewModel = new BannerViewModel();
        //    viewModel.IID = ID;
        //    viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BannerType", Url = "Mutual/GetLookUpData?lookType=BannerType" });
        //    viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BannerStatus", Url = "Mutual/GetLookUpData?lookType=BannerStatus" });
        //    viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ActionLinkType", Url = "Mutual/GetLookUpData?lookType=ActionLinkType" });
        //    TempData["viewModel"] = viewModel;
        //    return RedirectToAction("Create", "CRUD");
        //}

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var dto = ClientFactory.BannerServiceClient(CallContext).GetBanner(ID.ToString());
            var vm = BannerViewModel.FromDTO(dto);
            vm.BannerUrl  = string.Format("{0}//{1}//{2}",
                   ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.Banners.ToString(), vm.BannerFile);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(BannerViewModel vm)
        {
            var userID = CallContext.LoginID;
            var fileName = System.IO.Path.GetFileName(vm.BannerUrl);
            //move from temparary folder to oringal location
            string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
                    ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Banners.ToString(), Constants.TEMPFOLDER, userID, fileName);
            string orignalFolderPath = string.Format("{0}//{1}//{2}",
                     ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Banners.ToString(), fileName);

            if (System.IO.File.Exists(tempFolderPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
                    Directory.CreateDirectory(orignalFolderPath);

                System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);
                vm.BannerFile = fileName;
            }

            var vm2 = BannerViewModel.FromDTO(ClientFactory.BannerServiceClient(CallContext).SaveBanner(BannerViewModel.ToDTO(vm)));

            //synch through FTP
            if (ConfigurationExtensions.GetAppConfigValue<bool>("IsSyncImageWithFtp"))
            {
                try
                {
                    var synchFiles  =new Dictionary<string, string>();
                    synchFiles.Add(orignalFolderPath, Path.Combine(EduegateImageTypes.Banners.ToString(), fileName));
                    Utility.SynchToFTPLocation(synchFiles, EduegateImageTypes.Banners);
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<BannerController>.Fatal(ex.Message, ex);
                }
            }

             vm2.BannerUrl = string.Format("{0}\\{1}\\{2}",
                   ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.Banners.ToString(), vm.BannerFile);
            return Json(vm2, JsonRequestBehavior.AllowGet);
        }

       

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }
    }
}