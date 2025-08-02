using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class TicketEntitilementDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int TicketEntitilementID { get; set; }
        [DataMember]
        public string TicketEntitilement1 { get; set; }
        [DataMember]
        public long? CountryAirportID { get; set; }
        [DataMember]
        public int? NoOfDays { get; set; }
        [DataMember]
        public KeyValueDTO CountryAirport { get; set; }
        [DataMember]
        public string CountryAirportShortName { get; set; }
    }
}
