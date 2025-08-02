using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFeedTypes", Schema = "feed")]
    public partial class DataFeedType
    {
        public DataFeedType()
        {
            DataFeedLogs = new HashSet<DataFeedLog>();
            DataFeedTables = new HashSet<DataFeedTable>();
        }

        [Key]
        public int DataFeedTypeID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string Name { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string TemplateName { get; set; }
        public byte? OperationID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string ProcessingSPName { get; set; }

        [ForeignKey("OperationID")]
        [InverseProperty("DataFeedTypes")]
        public virtual DataFeedOperation Operation { get; set; }
        [InverseProperty("DataFeedType")]
        public virtual ICollection<DataFeedLog> DataFeedLogs { get; set; }
        [InverseProperty("DataFeedType")]
        public virtual ICollection<DataFeedTable> DataFeedTables { get; set; }
    }
}
