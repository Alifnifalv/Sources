using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedOperation
    {
        public DataFeedOperation()
        {
            this.DataFeedTypes = new List<DataFeedType>();
        }

        public byte OperationID { get; set; }
        public string OperationName { get; set; }
        public virtual ICollection<DataFeedType> DataFeedTypes { get; set; }
    }
}
