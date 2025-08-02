namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.ViewColumns")]
    public partial class ViewColumn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ViewColumn()
        {
            ViewColumnCultureDatas = new HashSet<ViewColumnCultureData>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ViewColumnID { get; set; }

        public long? ViewID { get; set; }

        [StringLength(50)]
        public string ColumnName { get; set; }

        [StringLength(20)]
        public string DataType { get; set; }

        [StringLength(50)]
        public string PhysicalColumnName { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsVisible { get; set; }

        public bool? IsSortable { get; set; }

        public bool? IsQuickSearchable { get; set; }

        public int? SortOrder { get; set; }

        public bool? IsExpression { get; set; }

        [StringLength(1000)]
        public string Expression { get; set; }

        [StringLength(1000)]
        public string FilterValue { get; set; }

        public bool? IsExcludeForExport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViewColumnCultureData> ViewColumnCultureDatas { get; set; }

        public virtual View View { get; set; }
    }
}
