using Eduegate.Domain.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class BankStatementDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
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
