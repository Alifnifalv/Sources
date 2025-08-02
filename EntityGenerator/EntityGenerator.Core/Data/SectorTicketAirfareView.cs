using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SectorTicketAirfareView
    {
        public long SectorTicketAirfareIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidityFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidityTo { get; set; }
        public bool? IsTwoWay { get; set; }
        [StringLength(100)]
        public string Airport { get; set; }
        [StringLength(100)]
        public string ReturnAirport { get; set; }
        [StringLength(100)]
        public string FlightClass { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
    }
}
