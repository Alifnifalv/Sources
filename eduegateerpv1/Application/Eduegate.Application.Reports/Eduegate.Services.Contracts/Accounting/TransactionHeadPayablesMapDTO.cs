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
   public  class TransactionHeadPayablesMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long TransactionHeadPayablesMapIID { get; set; }
        [DataMember]
        public long PayableID { get; set; }
        [DataMember]
        public long HeadID { get; set; }        
    }
}
