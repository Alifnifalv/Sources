using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
    public class ProductSKUPriceSettingController : BaseSearchController
    {
        // GET: ProductSKUPriceSetting
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
        public ActionResult GetProductPriceLists()
        {
            List<KeyValueViewModel> kVMs = new List<KeyValueViewModel>();
            KeyValueViewModel kvm = null;

            var lst = ClientFactory.PriceSettingServiceClient(CallContext).GetProductPriceLists();

            if (lst.IsNotNull() && lst.Count > 0)
            {
                foreach (var ppl in lst)
                {
                    kvm = new KeyValueViewModel();

                    kvm.Key = ppl.ProductPriceListIID.ToString();
                    kvm.Value = ppl.PriceDescription;

                    kVMs.Add(kvm);
                }
            }

            return Json(kVMs);
        }

        [HttpGet]
        public ActionResult GetProductPriceListForSKU(long skuIID)
        {
            List<ProductPriceSKUDTO> dto = ClientFactory.PriceSettingServiceClient(CallContext).GetProductPriceListForSKU(skuIID);
            List<ProductPriceSKUViewModel> productPriceSKUs = dto.Select(x => ProductPriceSKUViewModel.FromDTO(x)).ToList();

            return Json(productPriceSKUs);
        }

        [HttpPost]
        public JsonResult SaveProductSKUPrice(List<ProductPriceSKUViewModel> sellingPriceSettings, long skuIID)
        {
            List<ProductPriceSKUViewModel> productPriceSkuVMs = new List<ProductPriceSKUViewModel>();
            bool result = false;

            try
            {
                if (sellingPriceSettings.IsNull())
                    sellingPriceSettings = new List<ProductPriceSKUViewModel>();

                List<ProductPriceSKUDTO> productPriceSKUs = ClientFactory.PriceSettingServiceClient(CallContext).SaveSKUPrice(sellingPriceSettings.Select(x => ProductPriceSKUViewModel.ToDTO(x)).ToList(), skuIID);

                if (productPriceSKUs.IsNotNull() && productPriceSKUs.Count > 0)
                {
                    productPriceSkuVMs = productPriceSKUs.Select(x => ProductPriceSKUViewModel.FromDTO(x)).ToList();
                }

                result = true;
            }
            catch (Exception exception)
            {
                LogHelper<ProductSKUPriceSettingController>.Fatal(exception.Message.ToString(), exception);
                return Json(result);
            }

            return Json(new { IsError = !result, ProductPriceSKUs = productPriceSkuVMs });
        }

        [HttpPost]
        public JsonResult SaveProductSKUPriceCustomerGroup(List<CustomerGroupViewModel> PriceCustomerGroupVMs, long IID)
        {
            List<CustomerGroupViewModel> CustomerGroupVMs = new List<CustomerGroupViewModel>();
            List<CustomerGroupPriceDTO> customerGroupPriceDTOs = new List<CustomerGroupPriceDTO>();
            CustomerGroupPriceDTO dto = null;
            bool result = false;
            long skuIID = 0;

            try
            {
                if (PriceCustomerGroupVMs.IsNotNull())

                    foreach (var cgm in PriceCustomerGroupVMs)
                    {
                        if (cgm.CustomerGroupPrices.IsNotNull() && cgm.CustomerGroupPrices.Count > 0)
                        {
                            foreach (var cgp in cgm.CustomerGroupPrices)
                            {
                                skuIID = (long)cgp.ProductSKUID;
                                customerGroupPriceDTOs.Add(ProductSKUPriceCustomerGroupViewModel.ToDTO(cgp));

                                if (cgp.QuantityPrice.IsNotNull() && cgp.QuantityPrice.Count > 0)
                                {
                                    foreach (var qp in cgp.QuantityPrice)
                                    {
                                        dto = new CustomerGroupPriceDTO();

                                        dto = ProductSKUPriceCustomerGroupViewModel.ToDTO(qp);
                                        dto.ProductPriceListID = cgp.ProductPriceListID;
                                        dto.CustomerGroupID = cgp.CustomerGroupID;
                                        dto.ProductSKUMapID = cgp.ProductSKUID;
                                        customerGroupPriceDTOs.Add(dto);
                                    }
                                }
                            }
                        }
                    }

                var custGroupPrices = ClientFactory.PriceSettingServiceClient(CallContext).SaveCustomerGroupPrice(customerGroupPriceDTOs, IID);

                CustomerGroupVMs = GetCustomerGroups(IID);
                result = true;
            }
            catch (Exception exception)
            {
                LogHelper<ProductSKUPriceSettingController>.Fatal(exception.Message.ToString(), exception);
                return Json(result);
            }

            return Json(new { IsError = !result, UpdatedData = CustomerGroupVMs });
        }

        [HttpGet]
        public ActionResult GetCustomerGroupPriceSettingsForSKU(long IID)
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
                List<CustomerGroupDTO> cgDTOs = ClientFactory.PriceSettingServiceClient(CallContext).GetCustomerGroupPriceSettingsForSKU(IID);

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
        [HttpGet]
        public ActionResult GetProductBranchLists()
        {
            List<KeyValueViewModel> kVMs = new List<KeyValueViewModel>();
            KeyValueViewModel kvm = null;

            var lst = ClientFactory.PriceSettingServiceClient(CallContext).GetProductBranchLists();

            if (lst.IsNotNull() && lst.Count > 0)
            {
                foreach (var ppl in lst)
                {
                    kvm = new KeyValueViewModel();

                    kvm.Key = ppl.BranchID.ToString();
                    kvm.Value = ppl.BranchName;

                    kVMs.Add(kvm);
                }
            }

            return Json(kVMs);
        }

        [HttpGet]
        public JsonResult GetProductPriceListStatus()
        {
            var lst = ClientFactory.PriceSettingServiceClient(CallContext).GetProductPriceListStatus();
            List<KeyValueViewModel> kVMs = new List<KeyValueViewModel>();
            KeyValueViewModel kvm = null;
            if (lst.IsNotNull() && lst.Count > 0)
            {
                foreach (var st in lst)
                {
                    kvm = new KeyValueViewModel();

                    kvm.Key = st.Key;
                    kvm.Value = st.Value;

                    kVMs.Add(kvm);
                }
            }

            return Json(kVMs); ;
        }
    }
}