using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("VIEWS_20210112", Schema = "setting")]
    public partial class VIEWS_20210112
    {
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
    }
}
