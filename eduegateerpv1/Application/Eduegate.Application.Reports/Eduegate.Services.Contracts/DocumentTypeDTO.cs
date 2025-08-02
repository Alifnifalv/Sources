using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class DocumentTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public Nullable<int> ReferenceTypeID { get; set; }
        [DataMember]
        public string TransactionTypeName { get; set; }
        [DataMember]
        public string System { get; set; }
        [DataMember]
        public string TransactionNoPrefix { get; set; }
        [DataMember]
        public Nullable<long> LastTransactionNo { get; set; }
        [DataMember]
        public Nullable<long> GLAccountID { get; set; }
        [DataMember]
        public List<SchedulerDTO> Schedulers { get; set; }
        [DataMember]
        public List<KeyValueDTO> Claims { get; set; }
        [DataMember]
        public List<KeyValueDTO> ClaimSets { get; set; }
        [DataMember]
        public KeyValueDTO ApprovalWorkflow { get; set; }
        [DataMember] 
        public List<DocumentTypeTypeDTO> DocumentMaps { get; set; }
        [DataMember] 
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public List<DocumentTypeTransactionNumberDTO> DocumentTypeTransactionNumbers { get; set; }
        [DataMember]
        public Nullable<int> TransactionSequenceType { get; set; }
        [DataMember]
        public Nullable<bool> IgnoreInventoryCheck { get; set; }
        [DataMember]
        public Nullable<int> TaxTamplateID { get; set; }
    }
}
