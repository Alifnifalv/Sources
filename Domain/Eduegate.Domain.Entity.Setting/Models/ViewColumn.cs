namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ViewColumns", Schema = "setting")]
    public partial class ViewColumn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ViewColumnID { get; set; }

        public long? ViewID { get; set; }

        [StringLength(50)]
        public string ColumnName { get; set; }

        [StringLength(20)]
        public string DataType { get; set; }

        [StringLength(50)]
        public string PhysicalColumnName { get; set; }

        public bool? IsExcludeForExport { get; set; }


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

        public virtual View View { get; set; }
    }
}
