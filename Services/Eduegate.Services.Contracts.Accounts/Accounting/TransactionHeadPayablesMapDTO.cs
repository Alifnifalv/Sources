using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
   public  class TransactionHeadPayablesMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long TransactionHeadPayablesMapIID { get; set; }
        [DataMember]
        public long PayableID { get; set; }
        [DataMember]
        public long HeadID { get; set; }        
    }
}
