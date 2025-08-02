using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFeedTables", Schema = "feed")]
    public partial class DataFeedTable
    {
        public DataFeedTable()
        {
            DataFeedTableColumns = new HashSet<DataFeedTableColumn>();
        }

        [Key]
        public int DataFeedTableID { get; set; }
        public int? DataFeedTypeID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string TableName { get; set; }

        [ForeignKey("DataFeedTypeID")]
        [InverseProperty("DataFeedTables")]
        public virtual DataFeedType DataFeedType { get; set; }
        [InverseProperty("DataFeedTable")]
        public virtual ICollection<DataFeedTableColumn> DataFeedTableColumns { get; set; }
    }
}
