using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BranchView
    {
        public long BranchIID { get; set; }
        public string BranchName { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<bool> IsMarketPlace { get; set; }
        public string StatusName { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> supplieriid { get; set; }
        public string Supplier { get; set; }
        public string RowCategory { get; set; }
    }
}
