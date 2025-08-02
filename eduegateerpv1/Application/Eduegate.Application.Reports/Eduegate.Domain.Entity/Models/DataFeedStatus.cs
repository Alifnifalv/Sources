using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedStatus
    {
        public DataFeedStatus()
        {
            this.DataFeedLogs = new List<DataFeedLog>();
        }

        public short DataFeedStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<DataFeedLog> DataFeedLogs { get; set; }
    }
}
