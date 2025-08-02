using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFeedStatuses", Schema = "feed")]
    public partial class DataFeedStatus
    {
        public DataFeedStatus()
        {
            DataFeedLogs = new HashSet<DataFeedLog>();
        }

        [Key]
        public short DataFeedStatusID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }

        [InverseProperty("DataFeedStatus")]
        public virtual ICollection<DataFeedLog> DataFeedLogs { get; set; }
    }
}
