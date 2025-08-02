using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.OrderHistory
{
    [DataContract]
    public class EditOrderDetailDTO
    {
        [DataMember]
        public ReplacementActions Action { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}
