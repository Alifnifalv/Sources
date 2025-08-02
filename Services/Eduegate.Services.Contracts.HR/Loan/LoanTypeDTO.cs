
using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Loan
{
    [DataContract]
    public partial class LoanTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LoanTypeDTO()
        {
            LoanHeads = new List<LoanHeadDTO>();
            LoanRequests = new List<LoanRequestDTO>();
        }

        [DataMember]
        public byte LoanTypeID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? DocumentTypeID { get; set; }

        [DataMember]
        public KeyValueDTO DocumentType { get; set; }
        [DataMember]
        public List<LoanHeadDTO> LoanHeads { get; set; }
        [DataMember]
        public List<LoanRequestDTO> LoanRequests { get; set; }
    }
}
