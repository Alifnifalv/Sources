using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Framework.PageRendererEngine.ViewModels;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class BoilerplateController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Boilerplate";
            return View();
        }


        [HttpPost]
        public JsonResult Template(BoilerPlateInfoViewModel boilerPlateInfo)
        {
            //TODO: Need to verify why the boilerplate is comming 0
            if ((int)boilerPlateInfo.BoilerPlateID == 0)
            {
                return Json(null);
            }

            var boilerPlate = ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlate(((int)boilerPlateInfo.BoilerPlateID).ToString());
            var vm = new BoilerplateViewModel()
            {
                BoilerplateMapIID = boilerPlateInfo.BoilerPlateMapIID,
                BoilerplateID = (long)boilerPlateInfo.BoilerPlateID,
                RuntimeParameters = boilerPlateInfo.RuntimeParameters,
                Template = boilerPlate.Template
            };

            return Json(new { BoilerplateMapIID = boilerPlateInfo.BoilerPlateMapIID, Content = Eduegate.Framework.PageRendererEngine.RendererEngine.SetContexts(this.ControllerContext, this.HttpContext).Html(vm) });
        }

        [HttpPost]
        public JsonResult GetBoilerPlates(BoilerPlateInfoViewModel boilerPlateInfo)
        {
            var dto = ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlates(BoilerPlateInfoViewModel.ToDTO(boilerPlateInfo)); ;
            return Json(ToJsonString(dto, true), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBoilerPlatesWithDetails(BoilerPlateInfoViewModel boilerPlateInfo)
        {
            var dto = ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlates(BoilerPlateInfoViewModel.ToDTO(boilerPlateInfo)); ;
            return Json(new { boilerplate = boilerPlateInfo, resultData = ToJsonString(dto, true) }, JsonRequestBehavior.AllowGet);
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