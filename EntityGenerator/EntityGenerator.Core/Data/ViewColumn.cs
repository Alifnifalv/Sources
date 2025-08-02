using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ViewColumns", Schema = "setting")]
    [Index("ViewID", "IsDefault", Name = "id_viewColViewIDisDefault")]
    [Index("ViewID", Name = "idx_ViewColViewID")]
    public partial class ViewColumn
    {
        public ViewColumn()
        {
            ViewColumnCultureDatas = new HashSet<ViewColumnCultureData>();
        }

        [Key]
        public long ViewColumnID { get; set; }
        public long? ViewID { get; set; }
        [StringLength(50)]
        public string ColumnName { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string DataType { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PhysicalColumnName { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsSortable { get; set; }
        public bool? IsQuickSearchable { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsExpression { get; set; }
        public string Expression { get; set; }
        [StringLength(1000)]
        public string FilterValue { get; set; }
        public bool? IsExcludeForExport { get; set; }

        [ForeignKey("ViewID")]
        [InverseProperty("ViewColumns")]
        public virtual View View { get; set; }
        [InverseProperty("ViewColumn")]
        public virtual ICollection<ViewColumnCultureData> ViewColumnCultureDatas { get; set; }
    }
}
