using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobEntryDetail
    {
        public long JobEntryDetailIID { get; set; }
        public Nullable<long> JobEntryHeadID { get; set; }
        public Nullable<long> ProductSKUID { get; set; }
        public Nullable<long> ParentJobEntryHeadID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<bool> IsQuantiyVerified { get; set; }
        public Nullable<bool> IsBarCodeVerified { get; set; }
        public Nullable<bool> IsLocationVerified { get; set; }
        public Nullable<int> JobStatusID { get; set; }
        public Nullable<decimal> ValidatedQuantity { get; set; }
        public Nullable<int> ValidatedLocationID { get; set; }
        public string ValidatedPartNo { get; set; }
        public string ValidationBarCode { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string AWBNo { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual JobEntryHead JobEntryHead { get; set; }
        public virtual JobEntryHead JobEntryHead1 { get; set; }
        public virtual JobStatus JobStatus { get; set; }
    }
}
