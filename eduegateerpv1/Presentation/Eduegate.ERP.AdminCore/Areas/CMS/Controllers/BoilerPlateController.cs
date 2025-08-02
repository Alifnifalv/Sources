using Newtonsoft.Json;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Framework.PageRendererEngine.ViewModels;
using Eduegate.Framework.PageRendererEngine.Renderer;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.CMS
{
    [Area("CMS")]
    public class BoilerPlateController : BaseSearchController
    {
        // GET: BoilerPlate
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "BoilerPlate";
            viewModel.ListActionName = "BoilerPlate";
            viewModel.ViewModel = new BoilerPlateViewModel();
            viewModel.IID = ID;
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BranchGroup", Url = "Mutual/GetLookUpData?lookType=BranchGroup" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Warehouse", Url = "Mutual/GetLookUpData?lookType=Warehouse" });
            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("Create", "CRUD");
        }

        [HttpPost]
        public JsonResult Save([FromBody] BoilerPlateViewModel vm)
        {
            var userID = CallContext.LoginID;

            var result = ClientFactory.BoilerPlateServiceClient(CallContext).SaveBoilerPlate(BoilerPlateViewModel.ToDTO(vm));
            var vm2 = BoilerPlateViewModel.FromDTO(result);
            return Json(vm2);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }
        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.BoilerPlate);

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.BoilerPlate,
                ControllerName = "BoilerPlate",
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>"
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.BoilerPlate, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.BoilerPlateSummary);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var dto = ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlate(ID.ToString());
            var vm = BoilerPlateViewModel.FromDTO(dto);
           
            return Json(vm);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpPost]
        public async Task<IActionResult> Template(BoilerPlateInfoViewModel boilerPlateInfo)
        {
            //TODO: Need to verify why the boilerplate is comming 0
            if ((int)boilerPlateInfo.BoilerPlateID == 0)
            {
                return Ok(null);
            }

            //CallContext.EnableCache = true;
            var boilerPlate = ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlate(((int)boilerPlateInfo.BoilerPlateID).ToString());
            var htmlString = new StringBuilder();
            htmlString.Append(await this.RenderViewToStringAsync("/Areas/CMS/Views/Boilerplate/" + boilerPlate.Template + ".cshtml", boilerPlateInfo));
            return Ok(new { BoilerplateMapIID = boilerPlateInfo.BoilerPlateMapIID, Content = htmlString.ToString() });
        }

        [HttpPost]
        public async Task<IActionResult> GetBoilerPlates([FromBody] BoilerPlateInfoViewModel boilerPlateInfo)
        {
            var dto = await ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlates(BoilerPlateInfoViewModel.ToDTO(boilerPlateInfo)); ;
            return Json(ToJsonString(dto, true));
        }

        [HttpPost]
        public async Task<IActionResult> GetBoilerPlatesWithDetails([FromBody] BoilerPlateInfoViewModel boilerPlateInfo)
        {
            var dto = await ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlates(BoilerPlateInfoViewModel.ToDTO(boilerPlateInfo)); ;
            return Json( new { boilerplate = boilerPlateInfo, resultData = ToJsonString(dto, true) });
        }

        
       [HttpGet]
        public JsonResult GetBoilerPlateReports(long? boilerPlateID, long? pageBoilerPlateMapIID)
        {
            var result = ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlateReports(boilerPlateID, pageBoilerPlateMapIID);
            return Json(result);
        }


        private string ToJsonString(BoilerPlateDataSourceDTO dtos, bool isArray = true)
        {
            var jsonString = new StringBuilder();
            if (dtos.BoilerPlateResultSetList != null && dtos.BoilerPlateResultSetList.Count > 0)
            {
                var lastDTO = dtos.BoilerPlateResultSetList.Last();
                //if (isArray)
                //    jsonString.Append("[");
                jsonString.Append("{");
                foreach (var dto in dtos.BoilerPlateResultSetList)
                {
                    if (dto.Rows.Count > 0)
                    {
                        //isArray = true;
                        //if (dto.Rows.Count == 1)
                        //{
                        //    isArray = false;
                        //}
                        isArray = dto.IsArray;

                        jsonString.Append("\"" + dto.BoilerPlateName + "\":");
                        if (isArray)
                            jsonString.Append("[");
                        int i = 0;

                        foreach (var dataRow in dto.Rows)
                        {
                            jsonString.Append("{");

                            for (int j = 0; j < dto.Columns.Count; j++)
                            {
                                if (j < dto.Columns.Count - 1)
                                {
                                    jsonString.Append("\"" + dto.Columns[j].ColumnName.ToString()
                                 + "\":" + "\""
                                        //+ (dataRow.DataCells[j] == null ? string.Empty : HttpUtility.HtmlEncode((dataRow.DataCells[j].ToString().Replace("\t", "")))) + "\",");
                                 + (dataRow.DataCells[j] == null ? string.Empty : HttpUtility.HtmlEncode((escapeSpecialCharacters(dataRow.DataCells[j].ToString())))) + "\",");
                                }
                                else if (j == dto.Columns.Count - 1)
                                {
                                    jsonString.Append("\"" + dto.Columns[j].ColumnName.ToString()
                                 + "\":" + "\""
                                        //+ (dataRow.DataCells[j] == null ? string.Empty : HttpUtility.HtmlEncode(dataRow.DataCells[j].ToString().Replace("\t", ""))) + "\"");
                                 + (dataRow.DataCells[j] == null ? string.Empty : HttpUtility.HtmlEncode(escapeSpecialCharacters(dataRow.DataCells[j].ToString()))) + "\"");
                                }
                            }

                            if (i == dto.Rows.Count - 1)
                            {
                                jsonString.Append("}");
                            }
                            else
                            {
                                jsonString.Append("},");
                            }

                            i++;

                        }
                        if (isArray)
                            jsonString.Append("]");
                    }
                    if (dto != lastDTO && dto.Rows.Count > 0)
                    {
                        jsonString.Append(",");
                    }
                    else if (dto == lastDTO && dto.Rows.Count == 0)
                    {
                        int index = jsonString.ToString().LastIndexOf(',');
                        if (index != -1)
                        {
                            jsonString = new StringBuilder(jsonString.ToString().Remove(index, 1));//.Insert(index, "");
                        }
                        //jsonString.ToString().TrimEnd(',');
                    }

                }
                //if (isArray)
                //    jsonString.Append("]");
                jsonString.Append("}");
            }
            return jsonString.ToString();
        }

        private string escapeSpecialCharacters(string value)
        {
            string[] stringArray = new string[] { "\t", @"\" };
            string[] stringArrayReplace = new string[] { "", @"\\" };
            for (int i = 0; i <= stringArray.Length - 1; i++)
            {
                if (value.Contains(stringArray[i]))
                {
                    value = value.Replace(stringArray[i], stringArrayReplace[i]);
                }
            }
            return value;
        }
    }
}