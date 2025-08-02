using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataFeedTypes", Schema = "feed")]
    public partial class DataFeedType
    {
        public DataFeedType()
        {
            this.DataFeedLogs = new List<DataFeedLog>();
            this.DataFeedTables = new List<DataFeedTable>();
        }

        [Key]
        public int DataFeedTypeID { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
        public Nullable<byte> OperationID { get; set; }
        public string ProcessingSPName { get; set; }
        public virtual ICollection<DataFeedLog> DataFeedLogs { get; set; }
        public virtual DataFeedOperation DataFeedOperation { get; set; }
        public virtual ICollection<DataFeedTable> DataFeedTables { get; set; }
    }
}
