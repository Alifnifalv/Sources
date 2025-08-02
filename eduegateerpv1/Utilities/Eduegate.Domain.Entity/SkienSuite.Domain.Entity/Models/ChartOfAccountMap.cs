using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ChartOfAccountMap
    {
        public long ChartOfAccountMapIID { get; set; }
        public Nullable<long> ChartOfAccountID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public string Name { get; set; }
        public string AccountCode { get; set; }
        public Nullable<int> IncomeOrBalance { get; set; }
        public Nullable<int> ChartRowTypeID { get; set; }
        public Nullable<int> NoOfBlankLines { get; set; }
        public Nullable<bool> IsNewPage { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Account Account { get; set; }
        public virtual ChartOfAccount ChartOfAccount { get; set; }
        public virtual ChartRowType ChartRowType { get; set; }
    }
}
