using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataFeedStatuses", Schema = "feed")]
    public partial class DataFeedStatus
    {
        public DataFeedStatus()
        {
            this.DataFeedLogs = new List<DataFeedLog>();
        }

        [Key]
        public short DataFeedStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<DataFeedLog> DataFeedLogs { get; set; }
    }
}
