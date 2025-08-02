using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
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
            string imageHostUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl");
            var products = new List<ProductItemViewModel>();
            int dataSize = Convert.ToInt32(new Domain.Setting.SettingBL().GetSettingValue<string>("Select2DataSize"));

            try
            {
                products = ProductItemViewModel.FromDTOListToViewModelList(ClientFactory.ProductCatalogServiceClient(CallContext, null).ProductSKUSearch(searchText, dataSize, documentTypeID), imageHostUrl);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductSKUController>.Fatal(exception.Message.ToString(), exception);
            }

            return Json(new { LookUpName = lookupName, Data = products });
        }

        [HttpGet]
        public JsonResult ProductSKUByCategoryID(string lookupName, string categoryID = "")
        {
            string imageHostUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl");
            var products = new List<ProductItemViewModel>();
            int dataSize = Convert.ToInt32(new Domain.Setting.SettingBL().GetSettingValue<string>("Select2DataSize"));

            try
            {
                products = ProductItemViewModel.FromDTOListToViewModelList(ClientFactory.ProductCatalogServiceClient(CallContext, null).ProductSKUByCategoryID(long.Parse(categoryID), dataSize), imageHostUrl);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductSKUController>.Fatal(exception.Message.ToString(), exception);
            }

            return Json(new { LookUpName = lookupName, Data = products });
        }

        [HttpGet]
        public long SOSearch(string searchText = "")
        {
            long transactionID = 0;
            try
            {
                transactionID = ClientFactory.ProductCatalogServiceClient(CallContext, null).SOSearch(searchText);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductSKUController>.Fatal(exception.Message.ToString(), exception);
            }

            return transactionID;
        }

        [HttpGet]
        public JsonResult ProductSKUSearchUsingBarcode(string lookupName, string searchText = "", string documentTypeID = "")
        {
            string imageHostUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl");
            var products = new List<ProductItemViewModel>();
            int dataSize = Convert.ToInt32(new Domain.Setting.SettingBL().GetSettingValue<string>("Select2DataSize"));

            try
            {
                var searchedproducts = ProductItemViewModel.FromDTOListToViewModelList(ClientFactory.ProductCatalogServiceClient(CallContext, null).ProductSKUSearch(searchText, dataSize, documentTypeID), imageHostUrl);
                products = searchedproducts.Where(a => a.BarCode == searchText).ToList();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductSKUController>.Fatal(exception.Message.ToString(), exception);
            }

            return Json(new { LookUpName = lookupName, Data = products });
        }
    }
}