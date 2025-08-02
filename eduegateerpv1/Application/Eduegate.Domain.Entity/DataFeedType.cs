namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feed.DataFeedTypes")]
    public partial class DataFeedType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataFeedType()
        {
            DataFeedLogs = new HashSet<DataFeedLog>();
            DataFeedTables = new HashSet<DataFeedTable>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DataFeedTypeID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string TemplateName { get; set; }

        public byte? OperationID { get; set; }

        [StringLength(250)]
        public string ProcessingSPName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataFeedLog> DataFeedLogs { get; set; }

        public virtual DataFeedOperation DataFeedOperation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataFeedTable> DataFeedTables { get; set; }
    }
}
