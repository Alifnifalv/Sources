namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feed.DataFeedStatuses")]
    public partial class DataFeedStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataFeedStatus()
        {
            DataFeedLogs = new HashSet<DataFeedLog>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short DataFeedStatusID { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataFeedLog> DataFeedLogs { get; set; }
    }
}
