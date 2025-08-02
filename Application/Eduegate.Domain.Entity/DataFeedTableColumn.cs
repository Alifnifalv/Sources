namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feed.DataFeedTableColumns")]
    public partial class DataFeedTableColumn
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DataFeedTableColumnID { get; set; }

        public int DataFeedTableID { get; set; }

        [StringLength(250)]
        public string PhysicalColumnName { get; set; }

        [StringLength(250)]
        public string DisplayColumnName { get; set; }

        [StringLength(20)]
        public string DataType { get; set; }

        public bool? IsPrimaryKey { get; set; }

        public byte? SortOrder { get; set; }

        public virtual DataFeedTable DataFeedTable { get; set; }
    }
}
