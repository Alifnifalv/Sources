using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataFeedTables", Schema = "feed")]
    public partial class DataFeedTable
    {
        public DataFeedTable()
        {
            this.DataFeedTableColumns = new List<DataFeedTableColumn>();
        }

        [Key]
        public int DataFeedTableID { get; set; }
        public Nullable<int> DataFeedTypeID { get; set; }
        public string TableName { get; set; }
        public virtual ICollection<DataFeedTableColumn> DataFeedTableColumns { get; set; }
        public virtual DataFeedType DataFeedType { get; set; }
    }
}
