using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    public class CategoryController : BaseSearchController
    {
        [HttpGet]
        public ActionResult Create()
        {
            var vm = new ProductCategoryViewModel();

            //foreach (var dto in KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.CustomerGroup, string.Empty, 0, 0)))
            //{
            //    vm.PriceListsMap.Maps.Add(new CustomerGroupPriceListMapViewModel() { CustomerGroupID = long.Parse(dto.Key), GroupName = dto.Value });
            //}

            //foreach (var item in ClientFactory.CategoryServiceClient(CallContext).GetCategorySettings(vm.CategoryID))
            //{
            //    vm.CategorySettingMap.Maps.Add(new CategorySettingsViewModel()
            //    {
            //        CategorySettingsID = item.CategorySettingsID,
            //        CategoryID = item.CategoryID.HasValue ? item.CategoryID.Value : default(long),
            //        SettingCode = item.SettingCode,
            //        SettingValue = item.SettingValue,
            //        Description = item.Description
            //    });
            //}

            //vm.CategoryName.CultureDatas = CultureDataInfoViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).GetCultureList());
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = ProductCategoryViewModel.FromDTO(ClientFactory.ProductCatalogServiceClient(CallContext).GetProductCategory(ID.ToString()),
                CultureDataInfoViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).GetCultureList()));

            //foreach (var mapVm in vm.ImageMap.Maps)
            //{
            //    mapVm.ImageUrl = string.Format("{0}//{1}//{2}//{3}", ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.Category.ToString(),
            //        mapVm.ImageType == null ? string.Empty : mapVm.ImageType.ToString(), mapVm.ImageName);
            //}

            //if (vm.ImageMap.Maps.Count == 0)
            //    vm.ImageMap.Maps.Add(new ImageTypeMapViewModel() { });

            //foreach (var dto in KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.CustomerGroup, string.Empty, 0, 0)))
            //{
            //    if (!vm.PriceListsMap.Maps.Exists(a => a.CustomerGroupID.ToString() == dto.Key))
            //        vm.PriceListsMap.Maps.Add(new CustomerGroupPriceListMapViewModel() { CustomerGroupID = long.Parse(dto.Key), GroupName = dto.Value });
            //}

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(ProductCategoryViewModel vm)
        {
            var userID = CallContext.LoginID;
            var filesToSynch = new Dictionary<string, string>();

            //foreach (var mapVm in vm.ImageMap.Maps)
            //{
            //    var fileName = System.IO.Path.GetFileName(mapVm.ImageUrl);
            //    //move from temparary folder to oringal location
            //    string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
            //            ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Temporary.ToString(), Constants.TEMPFOLDER, userID, fileName);

            //    if (System.IO.File.Exists(tempFolderPath))
            //    {
            //        string orignalFolderPath = string.Format("{0}//{1}//{2}//{3}",
            //               ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Category.ToString(), mapVm.ImageType, fileName);
            //        var synchPathDirectory = Path.Combine(EduegateImageTypes.Category.ToString(), mapVm.ImageType, fileName);

            //        if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
            //            Directory.CreateDirectory(Path.GetDirectoryName(orignalFolderPath));

            //        System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);

            //        mapVm.ImageName = fileName;
            //        filesToSynch.Add(orignalFolderPath, synchPathDirectory);
            //    }
            //}

            var cultureInfo = CultureDataInfoViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).GetCultureList());

            var vm2 = ProductCategoryViewModel.FromDTO(ClientFactory.ProductCatalogServiceClient(CallContext).SaveProductCategory(ProductCategoryViewModel.ToDTO(vm,
               cultureInfo)), cultureInfo);
            //if (vm2.CategorySettingMap.Maps.Count == 0)
            //{
            //    foreach (var item in ClientFactory.CategoryServiceClient(CallContext).GetCategorySettings(vm.CategoryID))
            //    {
            //        vm2.CategorySettingMap.Maps.Add(new CategorySettingsViewModel()
            //        {
            //            CategorySettingsID = item.CategorySettingsID,
            //            CategoryID = item.CategoryID.HasValue ? item.CategoryID.Value : default(long),
            //            SettingCode = item.SettingCode,
            //            SettingValue = item.SettingValue,
            //            Description = item.Description
            //        });
            //    }
            //}
            //foreach (var mapVm2 in vm2.ImageMap.Maps)
            //{
            //    mapVm2.ImageUrl = string.Format("{0}//{1}//{2}//{3}", ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.Category.ToString(), mapVm2.ImageType, mapVm2.ImageName);
            //}

            if (ConfigurationExtensions.GetAppConfigValue<bool>("IsSyncImageWithFtp"))
            {
                try
                {
                    Utility.SynchToFTPLocation(filesToSynch, EduegateImageTypes.Category);
                }
                catch (Exception ex)
                {
                    LogHelper<CategoryController>.Fatal(ex.Message, ex);
                }
            }

            return Json(vm2, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCategories()
        {
            var categories = ClientFactory.ProductDetailServiceClient(CallContext).GetCategoryList();
            return Json(CategoryKeyValueViewModel(categories), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetReportingCategories()
        {
            var categories = ClientFactory.ProductDetailServiceClient(CallContext).GetReportingCategoryList();
            return Json(CategoryKeyValueViewModel(categories), JsonRequestBehavior.AllowGet);
        }

        public List<KeyValueViewModel> CategoryKeyValueViewModel(List<ProductCategoryDTO> categories)
        {
            var vM = new List<KeyValueViewModel>();

            foreach (var category in categories)
            {
                vM.Add(new KeyValueViewModel() { Key = category.CategoryID.ToString(), Value = category.CategoryName });
            }

            vM.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return vM;
        }
        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public ActionResult CategoryTags()
        {
            try
            {
                var tags = KeyValueViewModel.FromDTO(ClientFactory.CategoryServiceClient(this.CallContext).GetCategoryTags());
                return Json(tags, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper<CategoryController>.Fatal(ex.Message.ToString(), ex);
                throw;
            }
        }
    }
}