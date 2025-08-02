
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Loan
{
    [DataContract]
    public partial class LoanHeadDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public LoanHeadDTO()
        {
            LoanDetailDTOs = new List<LoanDetailDTO>();
        }

        [DataMember]
        public long LoanHeadIID { get; set; }
        [DataMember]
        public long? LoanRequestID { get; set; }
        [DataMember]
        public System.DateTime? LoanRequestDate { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }
        [DataMember]
        public byte? LoanTypeID { get; set; }
        [DataMember]
        public string LoanNo { get; set; }
        [DataMember]
        public string LoanRequestNo { get; set; }
        [DataMember]
        public byte? LoanStatusID { get; set; }
        [DataMember]
        public byte? LoanRequestStatusID { get; set; }
        [DataMember]
        public short? NoOfInstallments { get; set; }
        [DataMember]
        public DateTime? LoanDate { get; set; }
        [DataMember]
        public DateTime? PaymentStartDate { get; set; }
        [DataMember]
        public decimal? LoanAmount { get; set; }
        [DataMember]
        public decimal? Balance { get; set; }
        [DataMember]
        public decimal? InstallmentAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public DateTime? LastInstallmentDate { get; set; }
        [DataMember]
        public decimal? LastInstallmentAmount { get; set; }
        [DataMember]
        public DateTime? PaymentEndDate { get; set; }
        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public List<LoanDetailDTO> LoanDetailDTOs { get; set; }

        [DataMember]
        public string SponsorShip { get; set; }
    }
}
