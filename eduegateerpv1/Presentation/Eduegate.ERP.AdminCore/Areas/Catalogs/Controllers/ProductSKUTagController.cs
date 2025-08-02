using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Contracts.Common;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
    public class ProductSKUTagController : BaseSearchController
    {
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.ProductSKUTag);

            return View(new SearchListViewModel()
            {
                ControllerName = Infrastructure.Enums.SearchView.ProductSKUTag.ToString(),
                ViewName = Infrastructure.Enums.SearchView.ProductSKUTag,
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
                UserValues = metadata.UserValues,
                IsEditableLink = false,
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.ProductSKUTag, currentPage, orderBy);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }
        [HttpGet]
        public ActionResult GetProductSKUTagMaps(long productSKUMapIID)
        {
            List<KeyValueViewModel> selectedTags = new List<KeyValueViewModel>();
            var result = ClientFactory.ProductDetailServiceClient(CallContext).GetProductSKUTagMaps(productSKUMapIID);
            foreach (KeyValueDTO tag in result)
            {
                selectedTags.Add(new KeyValueViewModel() { Key = tag.Key, Value = tag.Value });
            }
            return Json(selectedTags);
        }

        [HttpPost]
        public ActionResult SaveProductSKUTags(TagViewModel model)
        {
            bool result = false;
            try
            {
                ProductSKUTagDTO dto = new ProductSKUTagDTO();
                dto.IDs = model.ProductSKUIDs;
                if (model.SelectedTags != null && model.SelectedTags.Count > 0)
                {
                    dto.SelectedTagIds = new List<long>();
                    dto.SelectedTagNames = new List<string>();
                    foreach (var tag in model.SelectedTags)
                    {
                        long tagID;
                        long.TryParse(tag.Key, out tagID);
                        dto.SelectedTagIds.Add(tagID);
                        dto.SelectedTagNames.Add(tag.Value);
                    }
                }
                result = ClientFactory.ProductDetailServiceClient(CallContext).SaveProductSKUTags(dto);
            }
            catch (Exception exception)
            {
                LogHelper<ProductSKUTagController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !result });
            }
            return Json(new { IsError = !result });

        }
    }
}