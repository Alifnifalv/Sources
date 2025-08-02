namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("DataTypes", Schema = "setting")]
    public partial class DataType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataType()
        {
            FilterColumnConditionMaps = new HashSet<FilterColumnConditionMap>();
            FilterColumns = new HashSet<FilterColumn>();
        }
        [Key]
        public byte DataTypeID { get; set; }

        [StringLength(50)]
        public string DateTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
    }
}
