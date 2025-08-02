using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.HR.Loan
{
    [DataContract]
    public partial class LoanEntryStatusDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LoanEntryStatusDTO()
        {
            LoanDetailDTOs = new HashSet<LoanDetailDTO>();
        }

        [DataMember]
        public byte LoanEntryStatusID { get; set; }
        [DataMember]
        public string StatusName { get; set; }  

        [DataMember]
        public virtual ICollection<LoanDetailDTO> LoanDetailDTOs { get; set; }
    }
}
