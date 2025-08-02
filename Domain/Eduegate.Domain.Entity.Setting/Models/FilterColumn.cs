namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("FilterColumns", Schema = "setting")]
    public partial class FilterColumn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FilterColumn()
        {
            FilterColumnConditionMaps = new HashSet<FilterColumnConditionMap>();
            FilterColumnUserValues = new HashSet<FilterColumnUserValue>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FilterColumnID { get; set; }

        public int? SequenceNo { get; set; }

        public long? ViewID { get; set; }

        [StringLength(50)]
        public string ColumnCaption { get; set; }

        [StringLength(50)]
        public string ColumnName { get; set; }

        public byte? DataTypeID { get; set; }

        public byte? UIControlTypeID { get; set; }

        public string DefaultValues { get; set; }

        public bool? IsQuickFilter { get; set; }

        public int? LookupID { get; set; }

        public string Attribute1 { get; set; }

        public string Attribute2 { get; set; }

        public bool? IsLookupLazyLoad { get; set; }

        public virtual DataType DataType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }

        public virtual UIControlType UIControlType { get; set; }

        public virtual View View { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
    }
}
