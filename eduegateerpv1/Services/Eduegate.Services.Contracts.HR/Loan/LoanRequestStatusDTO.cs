using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace Eduegate.Services.Contracts.HR.Loan
{
    [DataContract]
    public partial class LoanRequestStatusDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LoanRequestStatusDTO()
        {
            LoanHeads = new List<LoanHeadDTO>();
            LoanRequests = new List<LoanRequestDTO>();
        }

        [DataMember]
        public byte LoanRequestStatusID { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public  List<LoanHeadDTO> LoanHeads { get; set; }
        [DataMember]
        public List<LoanRequestDTO> LoanRequests { get; set; }
    }
}
