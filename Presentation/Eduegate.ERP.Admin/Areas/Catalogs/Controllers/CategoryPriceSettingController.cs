using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Infrastructure.Enums;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    public class CategoryPriceSettingController : BaseSearchController
    {
        // GET: CategoryPriceSetting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Eduegate.Services.Contracts.Enums.SearchView.CategoryPriceSetting);
            return View(new SearchListViewModel
            {
                ControllerName = SearchView.CategoryPriceSetting.ToString(),
                ViewName = SearchView.CategoryPriceSetting,
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
                IsEditableLink = false,
                UserValues = metadata.UserValues

            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(SearchView.CategoryPriceSetting, currentPage, orderBy);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(SearchView.CategorySummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public ActionResult GetCategoryPriceSettings(long categoryIID)
        {

            List<ProductPriceCategoryDTO> dto = ClientFactory.PriceSettingServiceClient(CallContext).GetCategoryPriceSettings(categoryIID);

            List<ProductPriceCategoryViewModel> categoryPriceSettings = dto.Select(x => ProductPriceCategoryViewModel.FromDTO(x)).ToList();

            return Json(categoryPriceSettings, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCategoryPriceSettings(List<ProductPriceCategoryViewModel> categoryPriceSettings, long categoryIID)
        {
            List<ProductPriceCategoryViewModel> categoryPriceVMs = new List<ProductPriceCategoryViewModel>();
            bool result = false;

            try
            {
                if (categoryPriceSettings.IsNull())

                    categoryPriceSettings = new List<ProductPriceCategoryViewModel>();
                List<ProductPriceCategoryDTO> categoryPrices = ClientFactory.PriceSettingServiceClient(CallContext).SaveCategoryPriceSettings(categoryPriceSettings.Select(x => ProductPriceCategoryViewModel.ToDTO(x)).ToList(), categoryIID);

                if (categoryPrices.IsNotNull() && categoryPrices.Count > 0)
                {
                    categoryPriceVMs = categoryPrices.Select(x => ProductPriceCategoryViewModel.FromDTO(x)).ToList();
                }
                result = true;
            }

            catch (Exception exception)
            {
                LogHelper<CategoryPriceSettingController>.Fatal(exception.Message.ToString(), exception);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsError = !result, CategoryPrices = categoryPriceVMs }, JsonRequestBehavior.AllowGet);
        }
    }
}