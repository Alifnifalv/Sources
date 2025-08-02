using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
    public class PriceController : BaseSearchController
    {
        private string productServiceUrl { get { return ServiceHost + Constants.PRODUCT_SERVICE_NAME; } }
        private static string dataSize { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("Select2DataSize"); } }
        private static string dateTimeFormat { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat"); } }
        // GET: Price
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.Price);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.Price.ToString(),
                ViewName = Infrastructure.Enums.SearchView.Price,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                IsChild = false,
                HasChild = false,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Infrastructure.Enums.SearchView.Price, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.PriceSummary);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return RedirectToAction("ProductPrice");
        }

        [HttpGet]
        public ActionResult ProductPrice()
        {
            var productPriceModel = new ProductPriceViewModel();
            var groups = ProductSKUPriceCustomerGroupViewModel.ToViewModels(
                KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(LookUpTypes.CustomerGroup, string.Empty, 0, 0)));
            productPriceModel.SKUPrice[0].CustomerGroups = groups;
            productPriceModel.CategoryPrice[0].CustomerGroups = groups;
            productPriceModel.BrandPrice[0].CustomerGroups = groups;
            productPriceModel.BranchMaps.Add(new ProductPriceBranchViewModel() { CustomerGroups = groups });

            return View("ProductPrice", productPriceModel);
        }

        [HttpPost]
        public JsonResult CreatePriceInformationDetail(ProductPriceViewModel productPriceInfo)
        {
            bool isSuccessful = false;
            var productPriceDTO = new ProductPriceDTO();
            string request = string.Empty;
            string SuccessMessage = string.Empty;

            try
            {
                if (productPriceInfo != null)
                {
                    productPriceDTO = ClientFactory.ProductDetailServiceClient(CallContext).CreatePriceInformationDetail(ProductPriceViewModel.ToDTO(productPriceInfo));
                    productPriceInfo = ProductPriceViewModel.FromDTO(productPriceDTO);
                    isSuccessful = true;
                }
            }
            catch (Exception exception)
            {
                LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !isSuccessful, Response = request, UserMessage = exception.Message.ToString() });
            }

            return Json(new { IsError = !isSuccessful, PriceModel = productPriceInfo });
        }



        [HttpGet]
        public ActionResult GetProductPriceList(string searchText)
        {
            var products = new List<POSProductViewModel>();

            try
            {
                //Getting all the product from the mentioned view
                var posProductItems = ClientFactory.ProductCatalogServiceClient(CallContext, null).GetProductListWithSKU(searchText, base.PageSize);

                //Getting all the sku maps linked the product list item
                var productPriceSKUMapDTOList = ClientFactory.ProductDetailServiceClient(CallContext).GetProductPriceSKUMaps();

                if (posProductItems.IsNotNull() && posProductItems.Count > 0)
                {
                    foreach (POSProductDTO posProduct in posProductItems)
                    {
                        var SKUMap = (from productPriceSKUMap in productPriceSKUMapDTOList
                                      where productPriceSKUMap.ProductSKUID == posProduct.ProductSKUMapIID
                                      select productPriceSKUMap).FirstOrDefault();

                        if (SKUMap == null) //if sku map iid is matching we are stopping the sku map item to load the data in the products select2 control
                        {
                            products.Add(new POSProductViewModel()
                            {
                                ProductIID = posProduct.ProductIID,
                                ProductSKUMapIID = posProduct.ProductSKUMapIID,
                                ProductName = posProduct.ProductName,
                                BarCode = posProduct.Barcode,
                                PartNo = posProduct.PartNo,
                                SKU = posProduct.SKU,
                                ProductPrice = Convert.ToInt64(posProduct.ProductPrice),
                                SellingQuantityLimit = posProduct.SellingQuantityLimit,
                                Sequence = Convert.ToInt32(posProduct.Sequence),
                                SortOrder = posProduct.SortOrder,
                                DeliveryCharge = posProduct.DeliveryCharge.HasValue ? posProduct.DeliveryCharge : 0,
                                Weight = posProduct.Weight,
                            });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
            }

            return Json(products);
        }

        [HttpPost]
        public JsonResult UpdateSKUProductPrice(List<ProductPriceSKUViewModel> productPriceSKUModelList)
        {
            bool result = false;
            List<ProductPriceSKUDTO> productPriceSKUDTOList = new List<ProductPriceSKUDTO>();

            try
            {
                if (productPriceSKUModelList != null && productPriceSKUModelList.Count > 0)
                {
                    foreach (var productPriceSKUViewModel in productPriceSKUModelList)
                    {
                        productPriceSKUDTOList.Add(new ProductPriceSKUDTO()
                        {
                            ProductPriceListItemMapIID = productPriceSKUViewModel.ProductPriceListItemMapIID,
                            ProductPriceListID = productPriceSKUViewModel.ProductPriceListID,
                            ProductSKUID = productPriceSKUViewModel.ProductSKUID,
                            //SellingQuantityLimit = productPriceSKUViewModel.SellingQuantityLimit,
                            //Amount = productPriceSKUViewModel.Amount,
                            //SortOrder = productPriceSKUViewModel.SortOrder,
                            PricePercentage = productPriceSKUViewModel.PricePercentage,
                            //CustomerGroupID = string.IsNullOrEmpty(productPriceSKUViewModel.CustomerGroup) ? (long?)null : long.Parse(productPriceSKUViewModel.CustomerGroup),
                        });
                    }

                    productPriceSKUDTOList = ClientFactory.ProductDetailServiceClient(CallContext).UpdateSKUProductPrice(productPriceSKUDTOList);
                    result = true;
                }
            }
            catch (Exception exception)
            {
                LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !result, MapList = productPriceSKUDTOList, UserMessage = exception.Message.ToString() });
            }

            return Json(new { IsError = !result, MapList = productPriceSKUDTOList, Message = " Sucessfully saved." });
        }

        [HttpGet]
        public bool RemoveProductPriceListSKUMaps(long id)
        {
            return ClientFactory.ProductDetailServiceClient(CallContext).RemoveProductPriceListSKUMaps(id);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            ProductPriceViewModel productPriceModel = new ProductPriceViewModel();
            productPriceModel = GetProductPriceListDetail(id);
            return View("ProductPrice", productPriceModel);
        }

        public ProductPriceViewModel GetProductPriceListDetail(long id)
        {
            ProductPriceDTO dto = ClientFactory.ProductDetailServiceClient(CallContext).GetProductPriceListDetail(id);
            ProductPriceViewModel vm = ProductPriceViewModel.FromDTO(dto);
            return vm;
        }

        [HttpGet]
        public JsonResult GetProductPriceSKU(long id)
        {
            var dto = ClientFactory.ProductDetailServiceClient(CallContext).GetProductPriceSKU(id);
            var listlistProductPriceSKUViewModel = dto.Select(x => ProductPriceSKUViewModel.FromDTO(x)).ToList();
            return Json(new { ProductPriceSKUViewModel = listlistProductPriceSKUViewModel });
        }

        [HttpGet]
        public ActionResult IndividualPrice()
        {
            return PartialView();
        }

        [HttpGet]
        public JsonResult GetProductPriceListTypes()
        {
            var dtos = ClientFactory.ProductDetailServiceClient(CallContext).GetProductPriceListTypes();
            var lists = dtos.Select(x => ProductPriceListTypeViewModel.ToViewModel(x)).ToList();
            return Json(new { ProductPriceListTypeViewModel = lists });
        }

        [HttpGet]
        public JsonResult GetProductPriceListLevels()
        {
            var dtos = ClientFactory.ProductDetailServiceClient(CallContext).GetProductPriceListLevels();
            var lists = dtos.Select(x => ProductPriceListLevelViewModel.ToViewModel(x)).ToList();
            return Json(new { ProductPriceListLevelViewModel = lists });
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        public ActionResult SKUPriceSettings(long SKUID = 0)
        {
            ViewBag.IID = SKUID;
            return View();
        }
    }
}