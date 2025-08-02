using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LeadSummaryView
    {
        public int? TotalLeads { get; set; }
        public int? TotalLead { get; set; }
        public int? TotalOpen { get; set; }
        public int? TotalMovetoAdmission { get; set; }
        public int? TotalWaiting { get; set; }
        public int? TotalClosed { get; set; }
        public int? TotalReject { get; set; }
    }
}
