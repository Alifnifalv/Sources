using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedTable
    {
        public DataFeedTable()
        {
            this.DataFeedTableColumns = new List<DataFeedTableColumn>();
        }

        public int DataFeedTableID { get; set; }
        public Nullable<int> DataFeedTypeID { get; set; }
        public string TableName { get; set; }
        public virtual ICollection<DataFeedTableColumn> DataFeedTableColumns { get; set; }
        public virtual DataFeedType DataFeedType { get; set; }
    }
}
