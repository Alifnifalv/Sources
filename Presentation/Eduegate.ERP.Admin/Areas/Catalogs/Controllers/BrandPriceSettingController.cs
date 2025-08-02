using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    public class BrandPriceSettingController : BaseSearchController
    {
        // GET: BrandPriceSetting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Eduegate.Services.Contracts.Enums.SearchView.BrandPriceSetting);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.BrandPriceSetting.ToString(),
                ViewName = Infrastructure.Enums.SearchView.BrandPriceSetting,
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
                IsEditableLink = false,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.BrandPriceSetting, currentPage, orderBy);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.BrandSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public ActionResult GetBrandpriceSettings(long brandIID)
        {
            List<ProductPriceBrandDTO> dto = ClientFactory.PriceSettingServiceClient(CallContext).GetBrandPriceSettings(brandIID);

            List<ProductPriceBrandViewModel> brandPriceSettings = dto.Select(x => ProductPriceBrandViewModel.FromDTO(x)).ToList();
            return Json(brandPriceSettings, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBrandPriceSettings(List<ProductPriceBrandViewModel> brandPriceSettings, long brandIID)
        {
            List<ProductPriceBrandViewModel> brandPriceVMs = new List<ProductPriceBrandViewModel>();
            bool result = false;

            try
            {
                if (brandPriceSettings.IsNull())

                    brandPriceSettings = new List<ProductPriceBrandViewModel>();
                List<ProductPriceBrandDTO> brandPrices = ClientFactory.PriceSettingServiceClient(CallContext).SaveBrandPriceSettings(brandPriceSettings.Select(x => ProductPriceBrandViewModel.ToDTO(x)).ToList(), brandIID);

                if (brandPrices.IsNotNull() && brandPrices.Count > 0)
                {
                    brandPriceVMs = brandPrices.Select(x => ProductPriceBrandViewModel.FromDTO(x)).ToList();
                }
                result = true;
            }

            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BrandPriceSettingController>.Fatal(exception.Message.ToString(), exception);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsError = !result, BrandPrices = brandPriceVMs }, JsonRequestBehavior.AllowGet);
        }
    }
}