using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedSearchView
    {
        public long DataFeedLogIID { get; set; }
        public string FileName { get; set; }
        public string FeedName { get; set; }
        public string StatusName { get; set; }
        public string RowCategory { get; set; }
        public string ImportedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> companyID { get; set; }
    }
}
