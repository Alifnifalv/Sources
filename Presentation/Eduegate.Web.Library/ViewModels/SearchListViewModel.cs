using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Services.Contracts.Search;
using Eduegate.Infrastructure.Enums;

namespace Eduegate.Web.Library.ViewModels
{
    public class SearchListViewModel
    {
        public SearchListViewModel()
        {
            IsEditableLink = true;
            HasChild = false;
            IsRowClickForMultiSelect = false;
            IsDetailedView = false;
            IsDeleteLink = false;
            IsLive = false;
            IsReloadSummarySmartViewAlways = false;
            HasSortable = false;
            EnabledSortableGrid = false;
            IsMasterDetail = false;
            ContextMenus = new List<KeyValueViewModel>();
            ViewActions = new List<ViewActionDTO>();
            QuickFilters = new List<ViewActionDTO>() { new ViewActionDTO() { ActionCaption= "All" } };
        }

        public string ControllerName { get; set; }
        public SearchView ViewName { get; set; }
        public string ViewTitle { get; set; }
        public string ViewFullPath { get; set; }
        public SearchView SummaryViewName { get; set; }

        public List<ColumnDTO> HeaderList { get; set; }
        public List<ColumnDTO> SummaryHeaderList { get; set; }

        public List<KeyValueDTO> SortColumns { get; set; }
        public List<KeyValueDTO> UserViews { get; set; }
        public List<ViewActionDTO> ViewActions { get; set; }
        public List<ViewActionDTO> QuickFilters { get; set; }

        public string SelectedSortColumn { get; set; }
        public string SelectedUserView { get; set; }

        public string InfoBar { get; set; }

        public bool IsMultilineEnabled { get;set; }
        public bool IsCategoryColumnEnabled { get; set; }

        public bool IsEditableLink { get; set; }
        public bool IsGenericCRUDSave { get; set; }
        public bool IsCommentLink { get; set; }
        public List<FilterColumnDTO> FilterColumns { get; set; }
        public List<FilterUserValueDTO> UserValues { get; set; }
        public string RuntimeFilter { get; set; }
        
        public SearchView ParentViewName { get; set; }
        public bool IsChild { get; set; }
        public bool HasChild { get; set; }
        public long? ChildView { get; set; }
        public string ChildFilterField { get; set; }

        public bool IsRowClickForMultiSelect {get; set;}
        public bool IsDetailedView { get; set; }

        public string ActionName { get; set; }
        //public SearchListViewModel ChildViewModel { get; set; }

        public bool IsDeleteLink { get; set; }
        public bool HasFilters { get; set; }
        public bool IsLive { get; set; }
        public bool IsReloadSummarySmartViewAlways { get; set; }

        public string JsControllerName { get; set; }

        public bool EnabledSortableGrid { get; set; }
        public bool HasSortable { get; set; }
        public string ActualControllerName { get; set; }
        public bool IsMasterDetail { get; set; }
        public List<KeyValueViewModel>  ContextMenus { get; set; }

        public string JsonModel
        {
            get
            {
                return JsonConvert.SerializeObject(FilterValueViewModel.FromModel(this.ViewName, FilterColumnViewModel.FromDTO(FilterColumns), UserValues));
            }
        }
    }
}



