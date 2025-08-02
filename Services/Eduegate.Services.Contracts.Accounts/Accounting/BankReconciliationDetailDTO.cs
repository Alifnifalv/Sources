using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class BankReconciliationDetailDTO : BaseMasterDTO
    {
        public BankReconciliationDetailDTO()
        {
            BankReconciliationMatchingEntry = new List<BankReconciliationMatchingEntryDTO>();
        }
        [DataMember]
        public long ReconciliationDetailIID { get; set; }
        [DataMember]
        public long BankReconciliationHeadID { get; set; }
        [DataMember]
        public long? TranHeadID { get; set; }
        [DataMember]
        public long? TranTailID { get; set; }
        [DataMember]
        public long? SlNo { get; set; }
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public DateTime? ReconciliationDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public int? StatusID { get; set; }
        [DataMember]
        public short? BankReconciliationMatchedStatusID { get; set; }
        [DataMember]
        public KeyValueDTO BankReconciliationMatchedStatus { get; set; }
        [DataMember]
        public long? BankStatementEntryID { get; set; }
        [DataMember]
        public string PartyName { get; set; }
        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public string ChequeDate { get; set; }
        [DataMember]
        public string ChequeNo { get; set; }
        [DataMember]
        public string ReferenceGroupNO { get; set; }
        [DataMember]
        public string ReferenceGroupName { get; set; }
        [DataMember]
        public virtual List<BankReconciliationMatchingEntryDTO> BankReconciliationMatchingEntry { get; set; }

    }
}
