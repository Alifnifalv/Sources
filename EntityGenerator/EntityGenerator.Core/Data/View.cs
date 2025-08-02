using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Views", Schema = "setting")]
    public partial class View
    {
        public View()
        {
            FilterColumnUserValues = new HashSet<FilterColumnUserValue>();
            FilterColumns = new HashSet<FilterColumn>();
            InverseChildView = new HashSet<View>();
            ScreenMetadatas = new HashSet<ScreenMetadata>();
            UserViews = new HashSet<UserView>();
            ViewActions = new HashSet<ViewAction>();
            ViewColumns = new HashSet<ViewColumn>();
            ViewCultureDatas = new HashSet<ViewCultureData>();
        }

        [Key]
        public long ViewID { get; set; }
        public byte? ViewTypeID { get; set; }
        [StringLength(50)]
        public string ViewName { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string ViewFullPath { get; set; }
        public bool? IsMultiLine { get; set; }
        public bool? IsRowCategory { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PhysicalSchemaName { get; set; }
        public bool? HasChild { get; set; }
        public bool? IsRowClickForMultiSelect { get; set; }
        public long? ChildViewID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ChildFilterField { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string ControllerName { get; set; }
        public bool? IsMasterDetail { get; set; }
        public bool? IsEditable { get; set; }
        public bool? IsGenericCRUDSave { get; set; }
        public bool? IsReloadSummarySmartViewAlways { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string JsControllerName { get; set; }
        [StringLength(1000)]
        public string ViewDescription { get; set; }
        [StringLength(100)]
        public string ViewTitle { get; set; }
        public bool? SecuredByLoginID { get; set; }
        public int? FilterQueriesID { get; set; }
        [StringLength(100)]
        public string ClaimTypeFilters { get; set; }
        public bool? HasAcademicYearFilter { get; set; }
        public bool? HasSchoolIDFilter { get; set; }
        public int? IsDeletable { get; set; }
        public byte? LayoutId { get; set; }

        [ForeignKey("ChildViewID")]
        [InverseProperty("InverseChildView")]
        public virtual View ChildView { get; set; }
        [ForeignKey("FilterQueriesID")]
        [InverseProperty("Views")]
        public virtual FilterQuery FilterQueries { get; set; }
        [ForeignKey("ViewTypeID")]
        [InverseProperty("Views")]
        public virtual ViewType ViewType { get; set; }
        [InverseProperty("View")]
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
        [InverseProperty("View")]
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
        [InverseProperty("ChildView")]
        public virtual ICollection<View> InverseChildView { get; set; }
        [InverseProperty("View")]
        public virtual ICollection<ScreenMetadata> ScreenMetadatas { get; set; }
        [InverseProperty("View")]
        public virtual ICollection<UserView> UserViews { get; set; }
        [InverseProperty("View")]
        public virtual ICollection<ViewAction> ViewActions { get; set; }
        [InverseProperty("View")]
        public virtual ICollection<ViewColumn> ViewColumns { get; set; }
        [InverseProperty("View")]
        public virtual ICollection<ViewCultureData> ViewCultureDatas { get; set; }
    }
}
