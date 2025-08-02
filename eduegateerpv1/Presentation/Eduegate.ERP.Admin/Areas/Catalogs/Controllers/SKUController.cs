using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    public class SKUController : BaseSearchController
    {
        // GET: SKU 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.SKU);

            return View(new SearchListViewModel()
            {
                ControllerName = Infrastructure.Enums.SearchView.SKU.ToString(),
                ViewName = Infrastructure.Enums.SearchView.SKU,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = true,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                            <input type='checkbox' ng-model='IsLive'/><i ng-class=""{'fa-spin': IsLive}"" class='live-spin fa fa-refresh'></i><span class='live-text'>Real time</span>
                        </li>",
                ActionName = "DetailedView",
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.SKU, currentPage, orderBy);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            var skuDTO = ClientFactory.ProductDetailServiceClient(CallContext).GetProductSKUDetails(ID);
            var productid = skuDTO.ProductID;
            return RedirectToAction("Edit", "Product", new { area = "Catalogs", ID = productid });
        }

        [HttpGet]
        public ActionResult GetProductSKUDetails(long IID = 0)
        {
            try
            {
                var skuDTO = ClientFactory.ProductDetailServiceClient(CallContext).GetProductSKUDetails(IID);
                var vm = SKUViewModel.FromDTOToVM(skuDTO);

                return Json(new { IsError = false, UserMessage = "", data = vm }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult SaveProductSKUDetails(SKUViewModel sku)
        {
            try
            {
                var skuDTO = ClientFactory.ProductDetailServiceClient(CallContext).SaveProductSKUDetails(SKUViewModel.ToDTO(sku));
                return Json(new { IsError = false, UserMessage = "", data = SKUViewModel.ToVM(skuDTO) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult ProductSearch(string searchText)
        {
            List<ProductMapViewModel> products = new List<ProductMapViewModel>();
            int dataSize = Convert.ToInt32(ConfigurationExtensions.GetAppConfigValue("Select2DataSize"));

            try
            {
                products = ProductMapViewModel.FromDTOToViewModel(ClientFactory.ProductCatalogServiceClient(CallContext).ProductSearch(searchText, dataSize));
            }
            catch (Exception exception)
            {
                LogHelper<SKUController>.Fatal(exception.Message.ToString(), exception);
            }

            return Json(new { Data = products }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveSKUProductMap(long productID, long skuID)
        {
            string result = string.Empty;
            bool isResult = false;
            try
            {
                result = ClientFactory.ProductDetailServiceClient(CallContext).SaveSKUProductMap(productID, skuID);
                isResult = string.IsNullOrEmpty(result) ? false : true;
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsError = !isResult, data = result, Message = "Product Mapped Successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveSKUManagers(long skuID, long ownerID)
        {
            bool result = false;
            try
            {
                AddProductDTO dto = new AddProductDTO();
                dto.ID = skuID;
                dto.SelectedKeyValueOwnerId = ownerID;

                result = ClientFactory.ProductDetailServiceClient(CallContext).SaveSKUManagers(dto);
            }
            catch (Exception exception)
            {
                LogHelper<ProductSKUTagController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsError = !result }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetEmployeeDetails(long ID)
        {
            try
            {
                var skuDTO = ClientFactory.ProductDetailServiceClient(CallContext).GetSKUEmployeeDetails(ID);
                var vm = AddProductViewModel.ToVM(skuDTO);

                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            return RedirectToAction("Product", "Product");
        }
    }
}