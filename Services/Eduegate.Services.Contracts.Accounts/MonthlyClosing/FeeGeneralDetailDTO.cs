using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.MonthlyClosing
{
    [DataContract]
    public class FeeGeneralDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public decimal? OpeningAmount { get; set; }

        [DataMember]
        public decimal? Debit { get; set; }

        [DataMember]
        public decimal? Credit { get; set; }

        [DataMember]
        public decimal? ClosingAmount { get; set; }
    }
}
