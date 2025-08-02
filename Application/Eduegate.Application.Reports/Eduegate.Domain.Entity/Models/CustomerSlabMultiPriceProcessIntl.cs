using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerSlabMultiPriceProcessIntl
    {
        public long CustomerSlabMultiPriceProcessIntlID { get; set; }
        public int RefCategoryID { get; set; }
        public string LogKey { get; set; }
        public int UserID { get; set; }
        public string SessionID { get; set; }
        public int TotalProducts { get; set; }
        public System.DateTime StartTime { get; set; }
        public int FinishedProducts { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public short CountryID { get; set; }
    }
}
