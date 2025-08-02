using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Vendor.PortalCore.Models;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Eduegate.Vendor.PortalCore.Controllers
{
    public class MutualController : BaseController
    {

        [HttpPost]
        public bool SaveFilter([FromBody] List<FilterValueViewModel> filter)
        {
            if (filter == null || filter.Count == 0) return true;
            var filterColumns = FilterColumnViewModel.FromDTO(ClientFactory.MetadataServiceClient(CallContext)
                .GetFilterMetadata((Services.Contracts.Enums.SearchView)(int)filter.First().View));

            var dto = new UserFilterValueDTO();
            dto.ColumnValues = new List<FilterColumnValueDTO>();
            dto.View = (Eduegate.Services.Contracts.Enums.SearchView)(int)filter.FirstOrDefault().View;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            //dto.LoginID = CallContext.
            foreach (var flt in filter.Where(a => a.IsDirty))
            {
                if (!string.IsNullOrEmpty(flt.FilterValue) || flt.FilterValue3 != null)
                {
                    var filterColumnInfo = filterColumns.FirstOrDefault(x => x.FilterColumnID == flt.FilterColumnID);
                    var value = flt.FilterValue3 != null && !string.IsNullOrEmpty(flt.FilterValue3.Value) ? flt.FilterValue3.Value : flt.FilterValue;
                    var value2 = flt.FilterValue2;
                    var filterCondition = flt.FilterCondition;


                    if (filterColumnInfo.ColumnType == Services.Contracts.Enums.DataTypes.Date)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            try
                            {
                                string inputFormat = "yyyy-MM-ddTHH:mm:ss.fffZ"; // Format for the input date  
                                // Parsing the date string to DateTime  
                                DateTime dateValue = DateTime.ParseExact(value, inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                                // Formatting to the desired output format  
                                value = dateValue.ToString(dateFormat, CultureInfo.InvariantCulture);
                                filterCondition = "Equals";
                            }
                            catch
                            {
                                value = null;
                            }
                        }

                        if (!string.IsNullOrEmpty(value2))
                        {
                            try
                            {
                                //conver the date into DB format
                                value2 = DateTime.ParseExact(value2, dateFormat, CultureInfo.InvariantCulture).ToShortDateString();
                            }
                            catch
                            {
                                value = null;
                            }
                        }
                    }

                    //TODO need to refactor as part of the header (not in the row)
                    dto.ColumnValues.Add(new FilterColumnValueDTO()
                    {
                        FilterColumnID = flt.FilterColumnID,
                        FilterCondition = (Eduegate.Services.Contracts.Enums.Conditions)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.Conditions), filterCondition),
                        Value = value,
                        Value2 = value2
                    });
                }
            }

            var result = ClientFactory.MetadataServiceClient(CallContext).SaveUserFilterMetadata(dto);
            return true;
        }

        [HttpGet]
        public JsonResult GetDynamicLookUpData(DynamicLookUpType lookType, bool defaultBlank = true, bool isBlank = false, string searchText = "", string lookupName = "")
        {
            if (isBlank)
            {
                return Json(new Framework.Web.Library.Common.KeyValueViewModel());
            }

            var VM = Framework.Web.Library.Common.KeyValueViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetDynamicLookUpData(lookType, searchText));

            if (defaultBlank)
            {
                VM.Insert(0, new Framework.Web.Library.Common.KeyValueViewModel() { Key = null, Value = string.Empty });
            }

            if (string.IsNullOrEmpty(lookupName))
            {
                return Json(VM);
            }
            else
            {
                return Json(new { LookUpName = lookupName, Data = VM });
            }
        }

        [HttpGet]
        public JsonResult GetSettingValueByKey(string settingKey)
        {
            try
            {
                var settingValue = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey(settingKey);
                return Json(settingValue);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MutualController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
