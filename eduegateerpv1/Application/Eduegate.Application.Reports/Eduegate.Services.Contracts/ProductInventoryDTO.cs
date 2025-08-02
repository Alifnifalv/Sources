using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Framework.Enums;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class ProductInventoryDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductSKUMapID { get; set; }
        [DataMember]
        public long TransactioHeadID { get; set; }
        [DataMember]
        public long TransactioDetailID { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public long? Batch { get; set; }
        [DataMember]
        public long? BranchID { get; set; }
        [DataMember]
        public long? ToBranchID { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [DataMember]
        public Nullable<decimal> CostPrice { get; set; }
        [DataMember]
        public DocumentReferenceTypes ReferenceTypes { get; set; }
        [DataMember]
        public List<string> SerialKeys { get; set; }
      
        [DataMember]
        public Nullable<decimal> Fraction { get; set; }

        [DataMember]
        public long? HeadID { get; set; }

        [DataMember]
        public Nullable<int> isActive { get; set; }

    }
}
