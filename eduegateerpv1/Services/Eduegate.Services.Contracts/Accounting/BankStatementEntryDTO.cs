using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class BankStatementEntryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public BankStatementEntryDTO() {
            BankReconciliationDetails = new List<BankReconciliationDetailDTO>();
        }

        [DataMember]
        public long BankStatementEntryIID { get; set; }
        [DataMember]
        public long BankStatementID { get; set; }
        [DataMember]
        public DateTime? PostDate { get; set; }
        [DataMember]
        public decimal? Debit { get; set; }
        [DataMember]
        public decimal? Credit { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string PartyName { get; set; }
        [DataMember]
        public string ReferenceNo { get; set; }
        [DataMember]
        public DateTime? ChequeDate { get; set; }
        [DataMember]
        public string ChequeNo { get; set; }
        [DataMember]
        public long? SlNO { get; set; }
        [DataMember]
        public decimal? Balance { get; set; }
        [DataMember]       
        public virtual List<BankReconciliationDetailDTO> BankReconciliationDetails { get; set; }
    }
}
