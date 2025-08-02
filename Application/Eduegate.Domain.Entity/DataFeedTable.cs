namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feed.DataFeedTables")]
    public partial class DataFeedTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataFeedTable()
        {
            DataFeedTableColumns = new HashSet<DataFeedTableColumn>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DataFeedTableID { get; set; }

        public int? DataFeedTypeID { get; set; }

        [StringLength(250)]
        public string TableName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataFeedTableColumn> DataFeedTableColumns { get; set; }

        public virtual DataFeedType DataFeedType { get; set; }
    }
}
