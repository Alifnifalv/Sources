using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Eduegate.Domain.Entity.HR.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("TicketEntitilementEntries", Schema = "payroll")]
    public partial class TicketEntitilementEntry
    {
        [Key]
        public long TicketEntitilementEntryIID { get; set; }
        public long? EmployeeID { get; set; }
        public int? TicketEntitilementDays { get; set; }
        public bool? IsTicketFareIssued { get; set; }
        public bool? IsTicketFareReimbursed { get; set; }
        public decimal? TicketFareIssuedPercentage { get; set; }
        public decimal? TicketFareReimbursementPercentage { get; set; }
        public int? VacationDaysEveryYear { get; set; }
        public DateTime? VacationStartingDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TicketIssueDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TravelReturnAirfare { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TicketIssuedOrFareReimbursed { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LOPforTicketEntitilement { get; set; }
        public bool? IsConsidereLOP { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DaysToBeConsideredForLOP { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TicketEntitilementPer { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TicketfarePayable { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BalanceCarriedForwardPer { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BalanceTicketAmountPayable { get; set; }

        //[Column(TypeName = "decimal(18, 3)")]
        //public decimal BalanceBroughtForward { get; set; }
        public short? FlightClassID { get; set; }
        public bool? IsTwoWay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TicketEligibleFromDate { get; set; }
        public long? EmployeeCountryAirportID { get; set; }
        public short? TicketEntitlementEntryStatusID { get; set; }
        public bool? ISTicketEligible { get; set; }
        [StringLength(50)]
        public string GenerateTravelSector { get; set; }
        public int? TicketEntitilementID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastTicketGivenDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        // public byte[] TimeStamps { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("TicketEntitilementEntries")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("FlightClassID")]
        [InverseProperty("TicketEntitilementEntries")]
        public virtual FlightClass FlightClass { get; set; }
        [ForeignKey("TicketEntitilementID")]
        [InverseProperty("TicketEntitilementEntries")]
        public virtual TicketEntitilement TicketEntitilement { get; set; }
        public virtual TicketEntitlementEntryStatus TicketEntitlementEntryStatus { get; set; }
        [ForeignKey("EmployeeCountryAirportID")]
        [InverseProperty("TicketEntitilementEntries")]
        public virtual Airport EmployeeCountryAirport { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BalanceBroughtForward { get; set; }


    }
}
