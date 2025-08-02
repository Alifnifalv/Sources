using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class View
    {
        public View()
        {
            this.FilterColumns = new List<FilterColumn>();
            this.FilterColumnUserValues = new List<FilterColumnUserValue>();
            this.ScreenMetadatas = new List<ScreenMetadata>();
            this.UserViews = new List<UserView>();
            this.ViewColumns = new List<ViewColumn>();
            this.Views1 = new List<View>();
        }

        public long ViewID { get; set; }
        public Nullable<byte> ViewTypeID { get; set; }
        public string ViewName { get; set; }
        public string ViewFullPath { get; set; }
        public Nullable<bool> IsMultiLine { get; set; }
        public Nullable<bool> IsRowCategory { get; set; }
        public string PhysicalSchemaName { get; set; }
        public Nullable<bool> HasChild { get; set; }
        public Nullable<bool> IsRowClickForMultiSelect { get; set; }
        public Nullable<long> ChildViewID { get; set; }
        public string ChildFilterField { get; set; }
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
        public virtual ICollection<ScreenMetadata> ScreenMetadatas { get; set; }
        public virtual ICollection<UserView> UserViews { get; set; }
        public virtual ICollection<ViewColumn> ViewColumns { get; set; }
        public virtual ICollection<View> Views1 { get; set; }
        public virtual View View1 { get; set; }
        public virtual ViewType ViewType { get; set; }
    }
}
