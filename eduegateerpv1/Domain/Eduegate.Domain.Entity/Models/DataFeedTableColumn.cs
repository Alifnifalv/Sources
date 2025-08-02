using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataFeedTableColumns", Schema = "feed")]
    public partial class DataFeedTableColumn
    {
        [Key]
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
