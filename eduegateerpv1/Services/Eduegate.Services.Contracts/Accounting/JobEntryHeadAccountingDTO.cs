using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Catalog;
namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class JobEntryHeadAccountingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long JobEntryHeadIID { get; set; }
        
        [DataMember]
        public Nullable<long> EmployeeID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
        [DataMember]
        public TransactionHeadDTO TransactionHeadDTO { get; set; }
    } 
}
