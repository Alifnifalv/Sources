namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("DataFormats", Schema = "setting")]
    public partial class DataFormat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataFormat()
        {
            UserDataFormatMaps = new HashSet<UserDataFormatMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DataFormatID { get; set; }

        public short? DataFormatTypeID { get; set; }

        [StringLength(100)]
        public string Format { get; set; }

        public bool? IsDefaultFormat { get; set; }

        public virtual DataFormatType DataFormatType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
    }
}
