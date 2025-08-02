using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
    public class ProductPriceSettingController : BaseSearchController
    {
        private string productDetailServiceUrl { get { return ServiceHost + Constants.PRODUCT_DETAIL_SERVICE_NAME; } }
        private string priceSettingsServiceUrl { get { return ServiceHost + Constants.PRICE_SETTING_SERVICE; } }

        // GET: ProductPrice
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Eduegate.Services.Contracts.Enums.SearchView.ProductPriceSetting);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.ProductPriceSetting.ToString(),
                ViewName = Infrastructure.Enums.SearchView.ProductPriceSetting,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                                        </div>
                                    </li>",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                IsChild = false,
                HasChild = true,
                IsEditableLink = false,
                IsReloadSummarySmartViewAlways = true,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Eduegate.Services.Contracts.Enums.SearchView.ProductSKU);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Infrastructure.Enums.SearchView.ProductSKUPriceSetting,
                ControllerName = Infrastructure.Enums.SearchView.ProductSKUPriceSetting.ToString(),
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("ProductIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.ProductPriceSetting,
                IsEditableLink = false,
                IsReloadSummarySmartViewAlways = true,
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.ProductPriceSetting;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Infrastructure.Enums.SearchView.ProductSKU;

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.ProductSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public ActionResult GetProductPriceListForSKU(long skuID = 0)
        {
            List<ProductPriceSKUDTO> dto;

            if (skuID == 0)
            {
                var lists = ClientFactory.PriceSettingServiceClient(CallContext).GetProductPriceLists();
                dto = new List<ProductPriceSKUDTO>();
                foreach (var list in lists)
                {
                    dto.Add(new ProductPriceSKUDTO()
                    {
                        ProductPriceListID = list.ProductPriceListIID,
                        PriceDescription = list.PriceDescription,
                        PricePercentage = list.PricePercentage,
                    });
                }
            }
            else
            {
                dto = ClientFactory.PriceSettingServiceClient(CallContext).GetProductPriceListForSKU(skuID);
            }

            return Json(dto);
        }

        [HttpGet]
        public ActionResult GetProductPriceSettings(long productIID)
        {
            List<ProductPriceSettingDTO> dto = ClientFactory.PriceSettingServiceClient(CallContext).GetProductPriceSettings(productIID);
            List<ProductPriceSettingViewModel> productPriceSettings = dto.Select(x => ProductPriceSettingViewModel.ToViewModel(x)).ToList();

            return Json(productPriceSettings);
        }

        [HttpPost]
        public JsonResult SaveProductPriceSettings(List<ProductPriceSettingViewModel> productPriceSettings, long productIID)
        {
            List<ProductPriceSettingViewModel> ppsVMs = new List<ProductPriceSettingViewModel>();
            bool result = false;

            try
            {
                if (productPriceSettings.IsNull())
                    productPriceSettings = new List<ProductPriceSettingViewModel>();

                List<ProductPriceSettingDTO> productPriceSKUs = ClientFactory.PriceSettingServiceClient(CallContext).SaveProductPrice(productPriceSettings.Select(x => ProductPriceSettingViewModel.ToDTO(x)).ToList(), productIID);

                if (productPriceSKUs.IsNotNull() && productPriceSKUs.Count > 0)
                {
                    ppsVMs = productPriceSKUs.Select(x => ProductPriceSettingViewModel.ToViewModel(x)).ToList();
                }

                result = true;
            }
            catch (Exception exception)
            {
                LogHelper<ProductSKUPriceSettingController>.Fatal(exception.Message.ToString(), exception);
                return Json(result);
            }

            return Json(new { IsError = !result, ProductPriceSettings = ppsVMs });
        }

        [HttpGet]
        public ActionResult GetPriceSettingCustomerGroups(long IID)
        {
            List<CustomerGroupViewModel> customerGroups = GetCustomerGroups(IID);
            return Json(customerGroups);
        }

        private List<CustomerGroupViewModel> GetCustomerGroups(long IID)
        {
            List<CustomerGroupViewModel> customerGroups = new List<CustomerGroupViewModel>();
            CustomerGroupViewModel cgVM = null;
            ProductSKUPriceCustomerGroupViewModel vm = null;

            if (IID > 0)
            {
                List<CustomerGroupDTO> cgDTOs = ClientFactory.PriceSettingServiceClient(CallContext).GetCustomerGroupPriceSettingsForProduct(IID);

                if (cgDTOs.IsNotNull() && cgDTOs.Count > 0)
                {
                    foreach (var cgDTO in cgDTOs)
                    {
                        cgVM = new CustomerGroupViewModel();
                        cgVM.CustomerGroupPrices = new List<ProductSKUPriceCustomerGroupViewModel>();

                        cgVM.CustomerGroupIID = cgDTO.CustomerGroupIID;
                        cgVM.GroupName = cgDTO.GroupName;

                        if (cgDTO.CustomerGroupPrices.IsNotNull() && cgDTO.CustomerGroupPrices.Count > 0)
                        {
                            var parentRows = cgDTO.CustomerGroupPrices.Where(x => x.Quantity.IsNull()).ToList();

                            foreach (var cgp in parentRows)
                            {
                                vm = new ProductSKUPriceCustomerGroupViewModel();

                                vm = ProductSKUPriceCustomerGroupViewModel.FromDTO(cgp);
                                var childRows = cgDTO.CustomerGroupPrices.Where(x => x.ProductPriceListID == cgp.ProductPriceListID && x.Quantity.IsNotNull()).ToList();

                                if (childRows.IsNotNull() && childRows.Count > 0)
                                {
                                    foreach (var child in childRows)
                                    {
                                        vm.QuantityPrice.Add(ProductSKUPriceCustomerGroupViewModel.FromDTO(child));
                                    }
                                }

                                cgVM.CustomerGroupPrices.Add(vm);
                            }
                        }

                        customerGroups.Add(cgVM);
                    }
                }
            }

            return customerGroups;
        }


        [HttpPost]
        public JsonResult SavePriceSettingCustomerGroups(List<CustomerGroupViewModel> PriceCustomerGroupVMs, long productIID)
        {
            List<CustomerGroupViewModel> CustomerGroupVMs = new List<CustomerGroupViewModel>();
            List<CustomerGroupPriceDTO> customerGroupPriceDTOs = new List<CustomerGroupPriceDTO>();
            CustomerGroupPriceDTO dto = null;
            bool result = false;

            try
            {
                if (PriceCustomerGroupVMs.IsNotNull())

                    foreach (var cgm in PriceCustomerGroupVMs)
                    {
                        if (cgm.CustomerGroupPrices.IsNotNull() && cgm.CustomerGroupPrices.Count > 0)
                        {
                            foreach (var cgp in cgm.CustomerGroupPrices)
                            {
                                customerGroupPriceDTOs.Add(ProductSKUPriceCustomerGroupViewModel.ToDTO(cgp));

                                if (cgp.QuantityPrice.IsNotNull() && cgp.QuantityPrice.Count > 0)
                                {
                                    foreach (var qp in cgp.QuantityPrice)
                                    {
                                        dto = new CustomerGroupPriceDTO();

                                        dto = ProductSKUPriceCustomerGroupViewModel.ToDTO(qp);
                                        dto.ProductPriceListID = cgp.ProductPriceListID;
                                        dto.CustomerGroupID = cgp.CustomerGroupID;
                                        dto.ProductID = cgp.ProductID;
                                        customerGroupPriceDTOs.Add(dto);
                                    }
                                }
                            }
                        }
                    }

                var custGroupPrices = ClientFactory.PriceSettingServiceClient(CallContext).SaveProductCustomerGroupPrice(customerGroupPriceDTOs, productIID);
                CustomerGroupVMs = GetCustomerGroups(productIID);
                result = true;
            }
            catch (Exception exception)
            {
                LogHelper<ProductSKUPriceSettingController>.Fatal(exception.Message.ToString(), exception);
                return Json(result);
            }

            return Json(new { IsError = !result, UpdatedData = CustomerGroupVMs });
        }

    }
}