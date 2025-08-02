using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class SeatingAvailabilityDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int? AllowSeatCapacity { get; set; }
        [DataMember]
        public int? MaximumSeatCapacity { get; set; }
        [DataMember]
        public int? SeatAvailability { get; set; }

        [DataMember]
        public int? SeatOccupied { get; set; }

        [DataMember]
        public string VehicleCode { get; set; }
    }
}
