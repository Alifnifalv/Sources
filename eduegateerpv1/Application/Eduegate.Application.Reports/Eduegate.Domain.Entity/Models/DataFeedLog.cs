using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedLog
    {
        public long DataFeedLogIID { get; set; }
        public Nullable<int> DataFeedTypeID { get; set; }
        public Nullable<short> DataFeedStatusID { get; set; }
        public string FileName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual DataFeedStatus DataFeedStatus { get; set; }
        public virtual DataFeedType DataFeedType { get; set; }
    }
}
