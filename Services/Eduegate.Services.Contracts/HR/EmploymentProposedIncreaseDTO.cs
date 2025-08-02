using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Contracts.HR
{
    [DataContract]
    public class EmploymentProposedIncreaseDTO : BaseMasterDTO
    {
        [DataMember]
        public decimal? ProposedIncrease { get; set; }

        [DataMember]
        public decimal? ProposedIncreasePercentage { get; set; }

        [DataMember]
        public decimal? ProposedIncreaseAmount { get; set; }

        [DataMember]
        public KeyValueDTO SalaryChangeAfterPeriod { get; set; }
        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public short REQ_LINE_NO { get; set; }

        [DataMember]
        public string CRE_BY { get; set; }
        [DataMember]
        public DateTime? CRE_DT { get; set; }
        [DataMember]
        public string CRE_PROG_ID { get; set; }
        [DataMember]
        public string CRE_IP { get; set; }
        [DataMember]
        public string CRE_WEBUSER { get; set; }
        [DataMember]
        public string UPD_BY { get; set; }
        [DataMember]
        public DateTime? UPD_DT { get; set; }
        [DataMember]
        public string UPD_PROG_ID { get; set; }
        [DataMember]
        public string UPD_IP { get; set; }
        [DataMember]
        public string UPD_WEBUSER { get; set; }
    }
}
