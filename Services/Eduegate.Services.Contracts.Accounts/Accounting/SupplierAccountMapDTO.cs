using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class SupplierAccountMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public string AccountName { get; set; }


        [DataMember]
        public List<SupplierAccountEntitlmentMapsDTO> SupplierAccountEntitlements { get; set; }

        
    }
}
