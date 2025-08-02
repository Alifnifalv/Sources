using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDisableQueryBatchLog
    {
        public int ProductDisableQueryBatchLogID { get; set; }
        public long BatchNo { get; set; }
        public int NoOfDays { get; set; }
        public int RecordsUpdated { get; set; }
        public long CreatedByID { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
