using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedTableColumn
    {
        public long DataFeedTableColumnID { get; set; }
        public int DataFeedTableID { get; set; }
        public string PhysicalColumnName { get; set; }
        public string DisplayColumnName { get; set; }
        public string DataType { get; set; }
        public Nullable<bool> IsPrimaryKey { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public virtual DataFeedTable DataFeedTable { get; set; }
    }
}
