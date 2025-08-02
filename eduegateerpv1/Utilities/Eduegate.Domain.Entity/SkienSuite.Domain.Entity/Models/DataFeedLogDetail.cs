using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedLogDetail
    {
        public long DataFeedLogDetailIID { get; set; }
        public Nullable<long> DataFeedLogID { get; set; }
        public string ErrorMessage { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
