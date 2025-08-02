using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Eduegate.Framework.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Search;
using Eduegate.Framework.Services;
using Eduegate.Framework.Mvc.ActionFitlers;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Infrastructure.Enums;
using System.Web.Routing;

namespace Eduegate.ERP.Admin.Controllers
{
    public class BaseSearchController : BaseController
    {        
        protected override void Execute(RequestContext requestContext)
        {
            ViewBag.CallContext = this.CallContext;
            base.Execute(requestContext);
        }

        protected string ServiceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }
        protected string PageSize { get { return ConfigurationExtensions.GetAppConfigValue("PageSize"); } }

        protected JsonResult SearchData(SearchView view, int currentPage, string orderBy, string runtimeFilter = "", int pageSize = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(orderBy) || orderBy.Equals("null"))
                {
                    orderBy = string.Empty;
                }

                pageSize = pageSize > 0 ? pageSize : int.Parse(PageSize);

                return Json(CustomJsonSerializer(ClientFactory.SearchServiceClient(CallContext).SearchDataPOST( new Eduegate.Services.Contracts.Search.SearchParameterDTO() {
                     SearchView = (Services.Contracts.Enums.SearchView)(int)view,
                      CurrentPage = currentPage,
                      PageSize = pageSize,
                      OrderBy = orderBy,
                      RuntimeFilter = runtimeFilter
                        })), JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BaseSearchController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }           
        }

        protected JsonResult SearchSummaryData(SearchView view)
        {
            var data = string.Empty;

            try
            {
                return Json(CustomJsonSerializer(ClientFactory.SearchServiceClient(CallContext).SearchData((Services.Contracts.Enums.SearchView)(int)view, 1, int.Parse(PageSize), string.Empty, string.Empty)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BaseSearchController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        protected string CustomJsonSerializer(SearchResultDTO resultDTO)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> eachRow;
            int index = 0;
            int actualCount = 0;
            foreach (var row in resultDTO.Rows)
            {
                index = 0;
                actualCount = 0;
                eachRow = new Dictionary<string, object>();
                eachRow.Add("IsRowSelected", false);

                foreach (var col in resultDTO.Columns)
                {
                    eachRow.Add(col.ColumnName == null ? "Col_" + actualCount.ToString() : col.ColumnName, index >= row.DataCells.Count? string.Empty : row.DataCells[index]);

                    if (col.ColumnName.IsNotNull()) // null column consider as icon action column which will not have any data.
                        index++;

                    actualCount++;
                }

                rows.Add(eachRow);
            }

            return JsonConvert.SerializeObject(new
            {
                Datas = rows,
                TotalRecords = resultDTO.TotalRecords,
                CurrentPage = resultDTO.CurrentPage,
                PageSize = resultDTO.PageSize,
                TotalPages = int.Parse(Math.Ceiling(resultDTO.TotalRecords / (resultDTO.PageSize <= 0 ? 1.0 : resultDTO.PageSize)).ToString()),
                GoToPageNo = resultDTO.CurrentPage
            });
        }
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}