using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class BankStatementDTO : BaseMasterDTO
    {
        public BankStatementDTO()
        {
            BankReconciliationHeadDtos = new List<BankReconciliationHeadDTO>();
            BankStatementEntries = new List<BankStatementEntryDTO>();
        }
        [DataMember]
        public long BankStatementIID { get; set; }
        [DataMember]
        public string ContentFileID { get; set; }
        [DataMember]
        public string ContentFileName { get; set; }
        [DataMember]
        public string ExtractedTextData { get; set; }
        [DataMember]
        public List<BankReconciliationHeadDTO> BankReconciliationHeadDtos { get; set; }
        [DataMember]
        public  List<BankStatementEntryDTO> BankStatementEntries { get; set; }
    }
}
