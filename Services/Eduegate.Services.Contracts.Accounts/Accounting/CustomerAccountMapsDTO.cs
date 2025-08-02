using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class CustomerAccountMapsDTO : BaseMasterDTO
    {
        [DataMember]
        public long CustomerAccountMapIID { get; set; }
        [DataMember]
        public long CustomerID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
       
    }
}
