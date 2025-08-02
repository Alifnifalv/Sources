using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Metadata;

namespace Eduegate.Services.Contracts.Search
{
    [DataContract]
    public class SearchGridMetadata
    {
        [DataMember]
        public List<ColumnDTO> SummaryColumns { get; set; }
        [DataMember]
        public List<ColumnDTO> Columns { get; set; }
        [DataMember]
        public List<ViewActionDTO> ViewActions { get; set; }
        [DataMember]
        public List<KeyValueDTO> SortColumns { get; set; }
        [DataMember]
        public List<KeyValueDTO> UserViews { get; set; }
        [DataMember]
        public string ViewName { get; set; }
        [DataMember]
        public string ViewTitle { get; set; }
        [DataMember]
        public string ViewFullPath { get; set; }
        [DataMember]
        public bool MultilineEnabled { get; set; }
        [DataMember]
        public bool RowCategoryEnabled { get; set; }
        [DataMember]
        public List<FilterColumnDTO> QuickFilterColumns { get; set; }
        [DataMember]
        public List<FilterUserValueDTO> UserValues { get; set; }
        [DataMember]
        public bool HasFilters { get; set; }

        [DataMember]
        public bool HasChild { get; set; }
        [DataMember]
        public long? ChildView { get; set; }
        [DataMember]
        public string ChildFilterField { get; set; }

        [DataMember]
        public bool IsRowClickForMultiSelect { get; set; }

        [DataMember]
        public string ControllerName { get; set; }
        [DataMember]
        public bool IsMasterDetail { get; set; }
        [DataMember]
        public bool IsEditable { get; set; }
        [DataMember]
        public bool IsGenericCRUDSave { get; set; }
        [DataMember]
        public bool IsReloadSummarySmartViewAlways { get; set; }
        [DataMember]
        public string JsControllerName { get; set; }
    }
}
