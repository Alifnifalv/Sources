using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;
namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class CollectFeeAccountMainDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long FeeCollectionFeeTypeMapsIID { get; set; }

        [DataMember]
        public long? FEECollectionID { get; set; }

        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public long? FeeMasterClassMapID { get; set; }



        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public virtual List<CollectFeeAccountDetailDTO> FeeAccountDetail { get; set; }
    }
}
