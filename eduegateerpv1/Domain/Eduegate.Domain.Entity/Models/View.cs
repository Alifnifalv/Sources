using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Views", Schema = "setting")]
    public partial class View
    {
        public View()
        {
            this.FilterColumns = new List<FilterColumn>();
            this.UserViews = new List<UserView>();
            this.ViewColumns = new List<ViewColumn>();
            this.ViewFilters = new List<ViewFilter>();
            this.FilterColumnUserValues = new List<FilterColumnUserValue>();
            this.ViewActions = new List<ViewAction>();
            ViewCultureDatas = new HashSet<ViewCultureData>();
        }

        [Key]
        public long ViewID { get; set; }

        public Nullable<byte> ViewTypeID { get; set; }

        public string ViewName { get; set; }

        public string ViewTitle { get; set; }

        public string ViewDescription { get; set; }

        public string ViewFullPath { get; set; }

        public Nullable<bool> IsMultiLine { get; set; }

        public Nullable<bool> IsRowCategory { get; set; }

        public Nullable<bool> HasChild { get; set; }

        public Nullable<long> ChildViewID { get; set; }

        public string ChildFilterField { get; set; }

        public Nullable<bool> IsRowClickForMultiSelect { get; set; }

        public string PhysicalSchemaName { get; set; }

        public string ControllerName { get; set; }

        public bool? IsMasterDetail { get; set; }

        public bool? IsEditable { get; set; }

        public bool? IsGenericCRUDSave { get; set; }

        public bool? IsReloadSummarySmartViewAlways { get; set; }

        public string JsControllerName { get; set; }

        public virtual ICollection<FilterColumn> FilterColumns { get; set; }

        public virtual ICollection<UserView> UserViews { get; set; }

        public virtual ICollection<ViewColumn> ViewColumns { get; set; }

        public virtual ICollection<ViewFilter> ViewFilters { get; set; }

        public virtual ICollection<ViewAction> ViewActions { get; set; }

        public virtual ViewType ViewType { get; set; }

        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewCultureData> ViewCultureDatas { get; set; }
    }
}