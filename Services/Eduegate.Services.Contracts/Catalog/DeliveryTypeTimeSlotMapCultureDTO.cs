using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class DeliveryTypeTimeSlotMapCultureDTO
    {
        [DataMember]
        public byte CultureID { get; set; }
        [DataMember]
        public long DeliveryTypeTimeSlotMapID { get; set; }
        [DataMember]
        public string CutOffDisplayText { get; set; }

    }
}
