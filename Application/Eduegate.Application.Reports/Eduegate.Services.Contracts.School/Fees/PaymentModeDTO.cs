using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class PaymentModeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public PaymentModeDTO()
        {
            Account = new KeyValueDTO();
            TenderType = new KeyValueDTO();
        }
        [DataMember]
        public int PaymentModeID { get; set; }

        [DataMember]
        public string PaymentModeName { get; set; }

        [DataMember]
        public long? AccountId { get; set; }
        
        [DataMember]
        public KeyValueDTO Account { get; set; }

        [DataMember]
        public int? TenderTypeID { get; set; }
        [DataMember]
        public KeyValueDTO TenderType { get; set; }
    }
}
