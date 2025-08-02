using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TicketEntitilementEntryView
    {
        public long TicketEntitilementEntryIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TicketIssueDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TravelReturnAirfare { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TicketfarePayable { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BalanceCarriedForwardPer { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BalanceTicketAmountPayable { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TicketEntitilementPer { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TicketIssuedOrFareReimbursed { get; set; }
        public bool? ISTicketEligible { get; set; }
        [StringLength(50)]
        public string TravelSector { get; set; }
        [Required]
        [StringLength(250)]
        public string Status { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsTicketFareIssued { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsTwoWay { get; set; }
        [StringLength(50)]
        public string Employeecode { get; set; }
        [StringLength(502)]
        public string Employee { get; set; }
    }
}
