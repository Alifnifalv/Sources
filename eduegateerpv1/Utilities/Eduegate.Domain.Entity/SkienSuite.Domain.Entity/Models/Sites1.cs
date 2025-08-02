using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Sites1
    {
        public long SiteID { get; set; }
        public string SiteName { get; set; }
        public Nullable<int> Created { get; set; }
        public Nullable<int> Updated { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
