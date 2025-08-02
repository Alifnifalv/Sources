using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataFeedOperations", Schema = "feed")]
    public partial class DataFeedOperation
    {
        public DataFeedOperation()
        {
            this.DataFeedTypes = new List<DataFeedType>();
        }

        [Key]
        public byte OperationID { get; set; }
        public string OperationName { get; set; }
        public virtual ICollection<DataFeedType> DataFeedTypes { get; set; }
    }
}
