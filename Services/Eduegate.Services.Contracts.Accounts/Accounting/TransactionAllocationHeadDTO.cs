using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public class TransactionAllocationHeadDTO : BaseMasterDTO
    {
        public TransactionAllocationHeadDTO()
        {
            TransactionAllocationDetailDTO = new List<TransactionAllocationDetailDTO>();
        }
        [DataMember]
        public long TransactionAllocationIID { get; set; }
        [DataMember]
        public DateTime? TransactionDate { get; set; }
        [DataMember]
        public string TransactionNumber { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public long? BranchID { get; set; }
        [DataMember]
        public long? CompanyID { get; set; }
        [DataMember]
        public int? TransactionStatusID { get; set; }
        [DataMember]
        public List<TransactionAllocationDetailDTO> TransactionAllocationDetailDTO { get; set; }
    }
}
