using System;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts.Jobs
{
    [DataContract]
    public class JobEntryHeadAccountingDTO : BaseMasterDTO
    {
        [DataMember]
        public long JobEntryHeadIID { get; set; }
        
        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public TransactionHeadDTO TransactionHeadDTO { get; set; }
    } 
}