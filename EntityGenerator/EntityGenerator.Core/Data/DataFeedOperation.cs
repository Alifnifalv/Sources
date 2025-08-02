using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFeedOperations", Schema = "feed")]
    public partial class DataFeedOperation
    {
        public DataFeedOperation()
        {
            DataFeedTypes = new HashSet<DataFeedType>();
        }

        [Key]
        public byte OperationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OperationName { get; set; }

        [InverseProperty("Operation")]
        public virtual ICollection<DataFeedType> DataFeedTypes { get; set; }
    }
}
