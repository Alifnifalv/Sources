using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Loan
{
    [DataContract]
    public partial class LoanDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LoanDetailID { get; set; }
        [DataMember]
        public long? LoanHeadID { get; set; }
        [DataMember]
        public DateTime? InstallmentDate { get; set; }
        [DataMember]
        public DateTime? InstallmentReceivedDate { get; set; }
        [DataMember]
        public decimal? InstallmentAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public bool? IsPaid { get; set; }
        [DataMember]
        public decimal? PaidAmount { get; set; }
        [DataMember]
        public byte?  LoanEntryStatusID { get; set; }
        [DataMember]
        public bool? IsDisableStatus { get; set; }
        [DataMember]
        public KeyValueDTO LoanEntryStatus { get; set; }
    }
}
