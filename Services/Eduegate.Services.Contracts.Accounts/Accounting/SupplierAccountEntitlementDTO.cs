using System;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class SupplierAccountEntitlmentMapsDTO : BaseMasterDTO
    {
        [DataMember]
        public Nullable<byte> EntitlementID { get; set; }
        [DataMember]
        public long SupplierAccountMapIID { get; set; }
        [DataMember]
        public long SupplierID { get; set; }
        [DataMember]
        public long AccountID { get; set; }
         
        [DataMember]
        public string EntitlementName { get; set; }
        [DataMember]
        public string AccountName { get; set; }
        [DataMember]
        public string AccountCode { get; set; }
        [DataMember]
        public AccountDTO Account { get; set; }
        [DataMember]
        public EntityTypes SupplierCustomer { get; set; } //1-Suppier 2- Customer
    }
}
