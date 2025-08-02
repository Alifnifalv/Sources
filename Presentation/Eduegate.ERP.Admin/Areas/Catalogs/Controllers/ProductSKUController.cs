using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    public class ProductSKUController : BaseSearchController
    {

        // GET: ProductSKU
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }


        [HttpGet]
        public JsonResult ProductSKUSearch(string lookupName, string searchText = "",string documentTypeID = "")
        {
            string imageHostUrl = ConfigurationExtensions.GetAppConfigValue("ImageHostUrl");
            var products = new List<ProductItemViewModel>();
            int dataSize = Convert.ToInt32(ConfigurationExtensions.GetAppConfigValue("Select2DataSize"));

            try
            {
                products = ProductItemViewModel.FromDTOListToViewModelList(ClientFactory.ProductCatalogServiceClient(CallContext).ProductSKUSearch(searchText, dataSize, documentTypeID), imageHostUrl);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductSKUController>.Fatal(exception.Message.ToString(), exception);
            }

            return Json(new { LookUpName = lookupName, Data = products }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ProductSKUByCategoryID(string lookupName, string categoryID = "")
        {
            string imageHostUrl = ConfigurationExtensions.GetAppConfigValue("ImageHostUrl");
            var products = new List<ProductItemViewModel>();
            int dataSize = Convert.ToInt32(ConfigurationExtensions.GetAppConfigValue("Select2DataSize"));

            try
            {
                products = ProductItemViewModel.FromDTOListToViewModelList(ClientFactory.ProductCatalogServiceClient(CallContext).ProductSKUByCategoryID(long.Parse(categoryID), dataSize), imageHostUrl);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductSKUController>.Fatal(exception.Message.ToString(), exception);
            }

            return Json(new { LookUpName = lookupName, Data = products }, JsonRequestBehavior.AllowGet);
        }
    }
}