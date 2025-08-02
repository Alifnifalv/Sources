using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedDetailView
    {
        public long DataFeedLogDetailIID { get; set; }
        public string ErrorMessage { get; set; }
        public Nullable<long> DataFeedLogIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
