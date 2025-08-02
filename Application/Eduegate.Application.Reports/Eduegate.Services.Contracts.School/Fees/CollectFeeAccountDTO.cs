using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class CollectFeeAccountDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public System.DateTime? CollectionDateFrom { get; set; }
        [DataMember]
        public System.DateTime? CollectionDateTo { get; set; }
        public List<CollectFeeAccountDetailDTO> DetailDataDto { get; set; }

        //public List<CollectFeeAccountMainDTO> MainDataDto { get; set; }
    }
}
