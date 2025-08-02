namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.Views")]
    public partial class View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public View()
        {
            FilterColumns = new HashSet<FilterColumn>();
            FilterColumnUserValues = new HashSet<FilterColumnUserValue>();
            ScreenMetadatas = new HashSet<ScreenMetadata>();
            UserViews = new HashSet<UserView>();
            ViewActions = new HashSet<ViewAction>();
            ViewColumns = new HashSet<ViewColumn>();
            ViewCultureDatas = new HashSet<ViewCultureData>();
            Views1 = new HashSet<View>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ViewID { get; set; }

        public byte? ViewTypeID { get; set; }

        [StringLength(50)]
        public string ViewName { get; set; }

        [StringLength(1000)]
        public string ViewFullPath { get; set; }

        public bool? IsMultiLine { get; set; }

        public bool? IsRowCategory { get; set; }

        [StringLength(50)]
        public string PhysicalSchemaName { get; set; }

        public bool? HasChild { get; set; }

        public bool? IsRowClickForMultiSelect { get; set; }

        public long? ChildViewID { get; set; }

        [StringLength(100)]
        public string ChildFilterField { get; set; }

        [StringLength(1000)]
        public string ControllerName { get; set; }

        public bool? IsMasterDetail { get; set; }

        public bool? IsEditable { get; set; }

        public bool? IsGenericCRUDSave { get; set; }

        public bool? IsReloadSummarySmartViewAlways { get; set; }

        [StringLength(1000)]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }

        public virtual FilterQuery FilterQuery { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenMetadata> ScreenMetadatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserView> UserViews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewAction> ViewActions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewColumn> ViewColumns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewCultureData> ViewCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<View> Views1 { get; set; }

        public virtual View View1 { get; set; }

        public virtual ViewType ViewType { get; set; }
    }
}
