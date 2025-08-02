using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class TransactionAllocationDTO
    {
        [DataMember]
        public long TransactionAllocationIID { get; set; }
        [DataMember]
        public Nullable<long> TrasactionDetailID { get; set; }
        [DataMember]
        public long BranchID { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [DataMember]
        public string TimeStamps { get; set; }
    }
}