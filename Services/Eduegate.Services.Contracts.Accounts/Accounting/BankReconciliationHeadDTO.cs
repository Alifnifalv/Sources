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
    public class    BankReconciliationHeadDTO : BaseMasterDTO
    {
        public BankReconciliationHeadDTO()
        {
            BankReconciliationDetailDtos = new List<BankReconciliationDetailDTO>();
            BankStatementEntryDTOs= new List<BankStatementEntryDTO>();
        }
        [DataMember]
        public long BankReconciliationHeadIID { get; set; }
        [DataMember]
        public long? BankStatementID { get; set; }
        [DataMember]
        public long? BankAccountID { get; set; }
        [DataMember]
        public DateTime? FromDate { get; set; }
        [DataMember]
        public DateTime? ToDate { get; set; }
        [DataMember]
        public decimal? OpeningBalanceAccount { get; set; }
        [DataMember]
        public decimal? OpeningBalanceBankStatement { get; set; }
        [DataMember]
        public decimal? ClosingBalanceAccount { get; set; }
        [DataMember]
        public decimal? ClosingBalanceBankStatement { get; set; }
        [DataMember]
        public short? BankReconciliationStatusID { get; set; }
        [DataMember]
        public KeyValueDTO BankReconciliationStatus { get; set; }
        [DataMember]
        public BankStatementDTO BankStatementData { get; set; }
        [DataMember]
        public List<BankReconciliationDetailDTO> BankReconciliationDetailDtos { get; set; }

        [DataMember]
        public List<BankStatementEntryDTO> BankStatementEntryDTOs { get; set; }
    }
}
