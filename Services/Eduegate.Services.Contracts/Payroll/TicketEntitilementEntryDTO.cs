using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class TicketEntitilementEntryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long TicketEntitilementEntryIID { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }
        [DataMember]
        public int? TicketEntitilementDays { get; set; }
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
        public DateTime? TicketIssueDate { get; set; }
        [DataMember]
        public decimal? TravelReturnAirfare { get; set; }
        [DataMember]
        public decimal? LOPforTicketEntitilement { get; set; }
        [DataMember]
        public decimal? TicketEntitilementPer { get; set; }
        [DataMember]
        public decimal? TicketIssuedOrFareReimbursed { get; set; }
        [DataMember]
        public decimal? TicketfarePayable { get; set; }
        [DataMember]
        public decimal? BalanceCarriedForwardPer { get; set; }
        [DataMember]
        public decimal? BalanceTicketAmountPayable { get; set; }
        [DataMember]
        public short? FlightClassID { get; set; }
        [DataMember]
        public bool? IsTwoWay { get; set; }
        [DataMember]
        public DateTime? TicketEligibleFromDate { get; set; }
        [DataMember]
        public DateTime? DateOfJoining { get; set; }
        [DataMember]
        public long? EmployeeCountryAirportID { get; set; }
        [DataMember]
        public bool? ISTicketEligible { get; set; }
        [DataMember]
        public string GenerateTravelSector { get; set; }
        [DataMember]
        public int? TicketEntitilementID { get; set; }
        [DataMember]
        public DateTime? LastTicketGivenDate { get; set; }
        [DataMember]
        public  KeyValueDTO Employee { get; set; }
        [DataMember]
        public KeyValueDTO EmployeeCountryAirport { get; set; }
        [DataMember]
        public  KeyValueDTO FlightClass { get; set; }
        [DataMember]
        public  KeyValueDTO TicketEntitilement { get; set; }
        [DataMember]
        public short? TicketEntitlementEntryStatusID { get; set; }
        [DataMember]
        public long? DepartmentID { get; set; }
        [DataMember]
        public bool? IsConsidereLOP { get; set; }
        [DataMember]
        public decimal? DaysToBeConsideredForLOP { get; set; }
        [DataMember]
        public decimal? BalanceBroughtForward { get; set; } = 0;
    }
}
