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
    public class EmployeeAirFareDTO : BaseMasterDTO
    {       
        [DataMember]
        public DateTime? TicketEligibleFromDate { get; set; }
        [DataMember]
        public long? EmployeeCountryAirportID { get; set; }
        [DataMember]
        public bool? ISTicketEligible { get; set; }
        [DataMember]
        public long? EmployeeNearestAirportID { get; set; }
        [DataMember]
        public int? TicketEntitilementID { get; set; }

        [DataMember]
        public int? TicketEntitilementDays { get; set; }
        [DataMember]
        public KeyValueDTO TicketEntitilement { get; set; }
        [DataMember]
        public KeyValueDTO EmployeeNearestAirport { get; set; }
        [DataMember]
        public KeyValueDTO EmployeeCountryAirport { get; set; }
        [DataMember]
        public string GenerateTravelSector { get; set; }
        [DataMember]
        public DateTime? LastTicketGivenDate { get; set; }

        [DataMember]
        public bool? IsTicketFareIssued { get; set; }
        [DataMember]
        public bool? IsTicketFareReimbursed { get; set; }
        [DataMember]
        public decimal? TicketFareIssuedPercentage { get; set; }
        [DataMember]
        public decimal? TicketFareReimbursementPercentage { get; set; }
        [DataMember]
        public int? VacationDaysEveryYear { get; set; }
        [DataMember]
        public DateTime? VacationStartingDate { get; set; }
        [DataMember]
        public DateTime?  TicketIssueDate { get; set; }

        [DataMember]
        public decimal? TravelReturnAirfare { get; set; }
        [DataMember]
        public decimal? TicketfarePayable { get; set; }
        [DataMember]
        public short? FlightClassID { get; set; }
        [DataMember]
        public KeyValueDTO FlightClass { get; set; }

        [DataMember]
        public bool? IsTwoWay { get; set; }
    }
}
