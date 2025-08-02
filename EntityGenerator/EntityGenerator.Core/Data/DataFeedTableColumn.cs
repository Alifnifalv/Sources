using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFeedTableColumns", Schema = "feed")]
    public partial class DataFeedTableColumn
    {
        [Key]
        public long DataFeedTableColumnID { get; set; }
        public int DataFeedTableID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string PhysicalColumnName { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string DisplayColumnName { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string DataType { get; set; }
        public bool? IsPrimaryKey { get; set; }
        public byte? SortOrder { get; set; }

        [ForeignKey("DataFeedTableID")]
        [InverseProperty("DataFeedTableColumns")]
        public virtual DataFeedTable DataFeedTable { get; set; }
    }
}
