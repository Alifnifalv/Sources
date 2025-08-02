using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Search;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Contracts.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using search = Eduegate.Services.Contracts.Search;

namespace Eduegate.Domain
{
    public class SearchBL
    {
        private CallContext _callContext { get; set; }

        public SearchBL(CallContext context)
        {
            _callContext = context;
        }

        public search.SearchResultDTO SearchData(Eduegate.Services.Contracts.Enums.SearchView view, int currentPage, int pageSize, string orderBy, string runtimeFilter, char viewType, byte? schoolID, int? academicYearID)
        {
            long totalRecords = 0;
            var dto = new search.SearchResultDTO();
            dto.Rows = new List<DataRowDTO>();
            dto.Columns = new List<search.ColumnDTO>();

            //var viewInfo = null;



            // TODO: 1. Move this to DataFeedController 2. all the suppliers mapped to branch should see feed 
            // To show data feed created by himself only if user is a marketplace user
            if ((long)view == (long)SearchView.DataFeed)
            {
                var supplierDetail = new SupplierBL(_callContext).GetSupplierByLoginID(Convert.ToInt64(_callContext.LoginID));

                if (supplierDetail.IsNotNull() && supplierDetail.IsMarketPlace == true)
                {
                    var userFilter = "CreatedBy=" + Convert.ToString(_callContext.LoginID);
                    if (runtimeFilter.IsNotNullOrEmpty())
                        runtimeFilter += "AND " + userFilter;
                    else
                        runtimeFilter += userFilter;
                }
            }

            var dataTable = new DataTable();
            switch ((SearchView)(long)view)
            {
                default:
                    dataTable = new SearchRepository().SearchData((SearchView)(long)view, ref totalRecords, currentPage, pageSize, orderBy, Convert.ToInt64(_callContext.LoginID), runtimeFilter,
                        _callContext != null && _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : 1, viewType, (byte?)_callContext.SchoolID, _callContext.AcademicYearID);
                    var viewInfo1 = new MetadataRepository().GetViewInfo((SearchView)(long)view, _callContext.LanguageCode);
                    if (viewInfo1.IsNotNull())
                    {
                        viewInfo1.ViewColumns = new List<ViewColumn>();
                        dto.Columns = GetMetadata((SearchView)(long)view, null, viewType);
                    }
                    break;
            }

            dto.TotalRecords = totalRecords;
            dto.PageSize = pageSize;
            dto.CurrentPage = currentPage;

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                // passing row data
                foreach (System.Data.DataRow item in dataTable.Rows)
                {
                    var cells = new DataCellListDTO();
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (item[i].GetType().Equals(typeof(DateTime)) && Convert.ToDateTime(item[i]).Year == 1900)
                        {
                            cells.Add(null);
                        }
                        else
                        {
                            // DB null should be handled
                            cells.Add(item.IsNull(i) ? null : item[i]);
                        }
                    }

                    dto.Rows.Add(new DataRowDTO() { DataCells = cells });
                }
            }

            //convert this table to DTO
            return dto;
        }

        public search.SearchGridMetadata SearchList(Eduegate.Services.Contracts.Enums.SearchView view)
        {
            var metadata = new search.SearchGridMetadata();
            var metadataRepository = new MetadataRepository();
            metadata.SortColumns = new List<KeyValueDTO>();
            metadata.UserViews = new List<KeyValueDTO>();

            var viewInfo = metadataRepository.GetViewInfo((SearchView)(long)view, _callContext.LanguageCode);

            if (viewInfo != null)
            {
                viewInfo.ViewColumns = new List<ViewColumn>();
                metadata.ViewName = viewInfo.ViewName;
                metadata.ViewTitle = viewInfo.ViewTitle;
                metadata.ViewFullPath = viewInfo.ViewFullPath;

                metadata.HasChild = viewInfo.HasChild.HasValue ? viewInfo.HasChild.Value : false;
                metadata.ChildView = viewInfo.ChildViewID;
                metadata.ChildFilterField = viewInfo.ChildFilterField;
                metadata.ControllerName = viewInfo.ControllerName;
                metadata.IsMasterDetail = viewInfo.IsMasterDetail.HasValue ? viewInfo.IsMasterDetail.Value : false;

                metadata.IsRowClickForMultiSelect = viewInfo.IsRowClickForMultiSelect.HasValue ? viewInfo.IsRowClickForMultiSelect.Value : false;
                metadata.MultilineEnabled = viewInfo.IsMultiLine.HasValue ? viewInfo.IsMultiLine.Value : false;
                metadata.IsEditable = viewInfo.IsEditable.HasValue ? viewInfo.IsEditable.Value : true;
                metadata.RowCategoryEnabled = viewInfo.IsRowCategory.HasValue ? viewInfo.IsRowCategory.Value : false;
                metadata.Columns = GetMetadata((SearchView)(long)view, metadata.SortColumns);
                metadata.SummaryColumns = GetMetadata((SearchView)((long)view + 1));
                var userViews = new SearchRepository().GetUserViews((SearchView)(long)view, _callContext == null ? null : _callContext.LoginID,
                    _callContext != null && _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : 1);
                metadata.ViewActions = new List<search.ViewActionDTO>();
                metadata.ViewActions.AddRange(viewInfo.ViewActions.Select(a => new search.ViewActionDTO()
                {
                    ViewID = a.ViewID,
                    ActionCaption = a.ActionCaption,
                    ViewActionID = a.ViewActionID,
                    ActionAttribute = a.ActionAttribute,
                    ActionAttribute2 = a.ActionAttribute2
                }));

                foreach (var column in userViews)
                {
                    metadata.UserViews.Add(new KeyValueDTO() { Key = column.UserViewIID.ToString(), Value = column.UserViewName });
                }

                metadata.QuickFilterColumns = new MetadataBL(_callContext).GetFilterMetadata(view, _callContext.LanguageCode).Where(a => a.IsQuickFilter == true).ToList();
                metadata.UserValues = new MetadataBL(_callContext).GetUserFilterMetadata(view);
                metadata.HasFilters = metadataRepository.HasUserFilter((SearchView)(long)view, _callContext == null ? null : _callContext.LoginID,
                    _callContext == null ? 1 : _callContext.CompanyID.Value);
                metadata.IsGenericCRUDSave = viewInfo.IsGenericCRUDSave.HasValue ? viewInfo.IsGenericCRUDSave.Value : false;
                metadata.IsReloadSummarySmartViewAlways = viewInfo.IsReloadSummarySmartViewAlways.HasValue ? viewInfo.IsReloadSummarySmartViewAlways.Value : false;
                metadata.JsControllerName = viewInfo.JsControllerName;
            }

            return metadata;
        }

        private List<search.ColumnDTO> GetMetadata(SearchView view, List<KeyValueDTO> sortColumns = null, char viewType = '\0')
        {
            var columns = new List<search.ColumnDTO>();
            var viewColumnsDe = new List<ViewColumn>();

            viewColumnsDe = new MetadataRepository().SearchColumns(view, viewType, _callContext.LanguageCode);
            foreach (var column in viewColumnsDe)
            {
                columns.Add(new search.ColumnDTO()
                {
                    Header = column.ColumnName,
                    ColumnName = column.PhysicalColumnName,
                    DataType = column.DataType,
                    IsExpression = column.IsExpression.HasValue ? column.IsExpression.Value : false,
                    Expression = column.Expression,
                    IsVisible = column.IsVisible.HasValue ? column.IsVisible.Value : true,
                    FilterValue = column.FilterValue
                });

                if (sortColumns != null && column.IsSortable.HasValue && column.IsSortable.Value)
                {
                    sortColumns.Add(new KeyValueDTO() { Key = column.PhysicalColumnName, Value = column.ColumnName });
                }
            }

            return columns;
        }
    }
}
