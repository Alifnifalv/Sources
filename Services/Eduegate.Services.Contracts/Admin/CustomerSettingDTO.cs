using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Admin
{
    [DataContract]
    public class CustomerSettingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CustomerSettingIID { get; set; }

        [DataMember]
        public bool? IsVerified { get; set; }
        [DataMember]
        public bool? IsConfirmed { get; set; }
        [DataMember]
        public bool? IsBlocked { get; set; }
        [DataMember]
        public decimal? CurrentLoyaltyPoints { get; set; }
        [DataMember]
        public decimal? TotalLoyaltyPoints { get; set; }

        [DataMember]
        public string CustomerGroup { get; set; }
    }
}
