using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductBarcodeBatchLog
    {
        public int ProductBarCodeBatchLogID { get; set; }
        public long BatchNo { get; set; }
        public int RecordsUploaded { get; set; }
        public Nullable<int> RecordsUpdated { get; set; }
        public Nullable<long> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
