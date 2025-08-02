using System;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.HR
{
    [DataContract]
    public class EmployementAllowanceDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public KeyValueDTO Allowance { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }


        [DataMember]
        public decimal? AmountAfterProbation { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string CRE_WEBUSER { get; set; }

        [DataMember]
        public string CRE_IP { get; set; }

        [DataMember]
        public string CRE_BY { get; set; }

        [DataMember]
        public DateTime? CRE_DT { get; set; }

        [DataMember]
        public DateTime? REQ_DT { get; set; }
    }
}
