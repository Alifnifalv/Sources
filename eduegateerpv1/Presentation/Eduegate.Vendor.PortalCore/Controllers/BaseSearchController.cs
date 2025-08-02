using Newtonsoft.Json;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Client.Factory;
using Eduegate.Infrastructure.Enums;
using Eduegate.Application.Mvc;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;
using System.Web;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Eduegate.Vendor.PortalCore.Controllers
{
    public class BaseSearchController : BaseController
    {
        //TODO: Remove this code after testing
        //protected override void Execute(RequestContext requestContext)
        //{
        //    ViewBag.CallContext = this.CallContext;
        //    base.Execute(requestContext);
        //}

        //protected string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        //protected string PageSize { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("PageSize"); } }

        protected string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        protected int PageSize { get { return new Domain.Setting.SettingBL().GetSettingValue<int>("PageSize", 1, 20); } }


        protected async Task<IActionResult> SearchData(SearchView view, int currentPage, string orderBy, string runtimeFilter = "", int pageSize = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(orderBy) || orderBy.Equals("null"))
                {
                    orderBy = string.Empty;
                }

                pageSize = pageSize > 0 ? pageSize : PageSize;

                var data = CustomJsonSerializer(await ClientFactory.SearchServiceClient(CallContext).SearchDataPOST( new Eduegate.Services.Contracts.Search.SearchParameterDTO() {
                     SearchView = (Services.Contracts.Enums.SearchView)(int)view,
                      CurrentPage = currentPage,
                      PageSize = pageSize,
                      OrderBy = orderBy,
                      RuntimeFilter = runtimeFilter
                        }));
                
                return Json(data);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BaseSearchController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, Message = ex.Message });
            }           
        }

        protected async Task<IActionResult> SearchSummaryData(SearchView view)
        {
            try
            {
                var data = CustomJsonSerializer(await ClientFactory.SearchServiceClient(CallContext).SearchData((Services.Contracts.Enums.SearchView)(int)view, 1, PageSize, string.Empty, string.Empty));
                return Ok(data);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BaseSearchController>.Fatal(ex.Message.ToString(), ex);
                return Ok(new { IsError = true, Message = ex.Message });
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

        //TODO: Remove this code after testing
        //protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    return new JsonNetResult(
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding,
        //        JsonRequestBehavior = behavior
        //    };
        //}

        public string EduegateCustomJsonSerializer(SearchResultDTO resultDTO)
        {
            var rows = new List<Dictionary<string, object>>();
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
                    var columnName = col.ColumnName == null ? "Col_" + actualCount.ToString() : col.ColumnName;
                    object data;

                    if (col.DataType == "jsonarray" && row.DataCells[index] != null)
                    {
                        data = JsonConvert.DeserializeObject(row.DataCells[index].ToString());
                        eachRow.Add(columnName, data);
                    }
                    else
                    {
                        data = index >= row.DataCells.Count ? string.Empty : row.DataCells[index];
                        eachRow.Add(columnName, HttpUtility.HtmlEncode(data));
                    }

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
    }
}