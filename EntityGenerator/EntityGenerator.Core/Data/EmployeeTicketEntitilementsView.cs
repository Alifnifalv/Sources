using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeeTicketEntitilementsView
    {
        public int TicketEntitilementID { get; set; }
        [StringLength(100)]
        public string TicketEntitilement { get; set; }
        public int? NoOfDays { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(100)]
        public string CountryAirport { get; set; }
    }
}
