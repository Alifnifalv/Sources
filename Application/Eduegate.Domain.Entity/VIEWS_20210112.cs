namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.VIEWS_20210112")]
    public partial class VIEWS_20210112
    {
        [Key]
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
    }
}
