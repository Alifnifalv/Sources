using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums.Accounting;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounting
{
   [DataContract]
   public  class TransactionHeadReceivablesMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long TransactionHeadReceivablesMapIID { get; set; }
        [DataMember]
        public long ReceivableID { get; set; }
        [DataMember]
        public long HeadID { get; set; }        
    }
}
