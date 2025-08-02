using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Payroll;
using Eduegate.Web.Library.HR.Payroll;
using Eduegate.Services.Client.Factory;
using Eduegate.Infrastructure.Enums;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Payroll;
using System.Linq;
using System.Globalization;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Web.Library.ViewModels.Filter;
using System.Data;
using System.Formats.Asn1;
using System.IO;
using System.Text;
using Eduegate.Domain.Content;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Contracts.School.Mutual;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Payroll;

namespace Eduegate.ERP.Admin.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class WPSController : BaseSearchController
    {
        public async Task<IActionResult> WPS()
        {
            var view = SearchView.WPS;
            var paramview = view;
            var viewModel = new FilterViewModel() { View = paramview, ViewName = paramview.ToString() };
            var filterClient = ClientFactory.MetadataServiceClient(CallContext);
            var metadata = filterClient.GetFilterMetadata((Eduegate.Services.Contracts.Enums.SearchView)view);
            viewModel.Columns = FilterColumnViewModel.FromDTO(metadata).OrderBy(x => x.FilterColumnID).ToList();
            viewModel.UserValues = filterClient.GetUserFilterMetadata((Services.Contracts.Enums.SearchView)(int)view);
            var gridMetadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)view);

            return View(new AdvanceFilterViewModel()
            {
                FilterViewModel = viewModel,
                SearchMetadata = new SearchListViewModel
                {
                    ViewName = paramview,
                    ViewTitle = gridMetadata.ViewName,
                    HeaderList = gridMetadata.Columns,
                    SummaryHeaderList = gridMetadata.SummaryColumns,
                    UserViews = gridMetadata.UserViews,
                    SortColumns = gridMetadata.SortColumns,
                    IsMultilineEnabled = gridMetadata.MultilineEnabled,
                    IsCategoryColumnEnabled = false,
                    InfoBar = string.Empty,
                    IsEditableLink = false,
                    ActualControllerName = gridMetadata.ControllerName,
                    ViewFullPath = gridMetadata.ViewFullPath,
                    RuntimeFilter = "",
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> GenerateWPS([FromBody] WPSDetailDTO wpsDTO)
        {
            string viewName = "WPS";
            Services.Contracts.Enums.SearchView searchView = (Services.Contracts.Enums.SearchView)Enum.Parse(typeof(Services.Contracts.Enums.SearchView), viewName);
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchData(searchView, 1, int.MaxValue, null, string.Empty, 'E');

            byte[] csvBytes = ToCSV(metadata, wpsDTO.CsvFileHeaders);

            //Save Csv file in Content Table 
            var savedContent = new ContentBL(CallContext).SaveFile(new Services.Contracts.Contents.ContentFileDTO()
            {
                ContentFileName = wpsDTO.FileName + ".csv",
                ContentData = csvBytes,
            });

            var contentFileID = savedContent?.ContentFileIID;

            //Save to wps detail table
            if (contentFileID != null)
            {
                wpsDTO.ContentID = contentFileID;
                var updatedVM = ClientFactory.EmployeeServiceClient(CallContext).SaveWPS(wpsDTO);
            }
            else
            {
                return null;
            }

            return Ok(contentFileID);
        }


        byte[] ToCSV(Eduegate.Services.Contracts.Search.SearchResultDTO dtDataTable, List<KeyValueDTO> headersList)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    #region HeaderData -- START
                    // Writing header as comma-separated
                    sw.WriteLine(string.Join(",", headersList.Select(item => item.Key)));
                    // Writing detail as comma-separated
                    sw.WriteLine(string.Join(",", headersList.Select(item => item.Value)));
                    #endregion HeaderData -- END

                    #region Grid Data START
                    sw.Write("Record Sequence,"); // Add the first column header
                    //headers
                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {
                        if (!dtDataTable.Columns[i].IsExcludeForExport)
                        {
                            sw.Write(dtDataTable.Columns[i].Header.ToString());
                            if (i < dtDataTable.Columns.Count - 1)
                            {
                                sw.Write(",");
                            }
                        }
                    }
                    sw.Write(sw.NewLine);

                    foreach (var dr in dtDataTable.Rows)
                    {
                        sw.Write((dtDataTable.Rows.IndexOf(dr) + 1).ToString().PadLeft(6, '0') + ",");

                        for (int i = 0; i < dtDataTable.Columns.Count; i++)
                        {
                            if (!dtDataTable.Columns[i].IsExcludeForExport)
                            {
                                if (!Convert.IsDBNull(dr.DataCells[i]))
                                {
                                    string value = dr.DataCells[i] == null ? "" : dr.DataCells[i].ToString();
                                    if (value.Contains(','))
                                    {
                                        value = String.Format("\"{0}\"", value);
                                        sw.Write(value);
                                    }
                                    else
                                    {
                                        sw.Write(dr.DataCells[i] == null ? "" : dr.DataCells[i].ToString());
                                    }
                                }
                                if (i < dtDataTable.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                        }
                        sw.Write(sw.NewLine);
                    }
                    #endregion Grid data END
                }
                return ms.ToArray();
            }
        }


        public async Task<IActionResult> GetTotalNetSalaries() 
        {
            string viewName = "WPS";
            Services.Contracts.Enums.SearchView searchView = (Services.Contracts.Enums.SearchView)Enum.Parse(typeof(Services.Contracts.Enums.SearchView), viewName);
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchData(searchView, 1, int.MaxValue, null, string.Empty, 'E');

            var dto = new SearchResultDTO()
            {
                TotalSalaries = metadata.TotalSalaries,
            };

            return Ok(dto.TotalSalaries);
        }

    }
}