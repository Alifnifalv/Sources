using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SearchKeyword
    {
        public long SearchKeyWordID { get; set; }
        public string Keyword { get; set; }
        public Nullable<System.DateTime> SearchOn { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<short> ResultCount { get; set; }
        public Nullable<byte> ResultPage { get; set; }
        public string SearchSql { get; set; }
        public Nullable<bool> IsCategory { get; set; }
        public string SortingValue { get; set; }
        public string CategoryOrBrandName { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public string IPCountry { get; set; }
    }
}
