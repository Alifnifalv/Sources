using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EntityChangeTrackerBatch
    {
        public int EntityChangeTrackerBatchID { get; set; }
        public string EntityChangeTrackerBatchNo { get; set; }
        public int NoOfProducts { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
