using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
   public  class TransactionHeadReceivablesMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long TransactionHeadReceivablesMapIID { get; set; }
        [DataMember]
        public long? ReceivableID { get; set; }
        [DataMember]
        public long? HeadID { get; set; }        
    }
}
