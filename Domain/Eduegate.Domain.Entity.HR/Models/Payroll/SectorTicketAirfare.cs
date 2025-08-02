using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.HR.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("SectorTicketAirfares", Schema = "payroll")]
    public partial class SectorTicketAirfare
    {
        [Key]
        public long SectorTicketAirfareIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidityFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidityTo { get; set; }
        public bool? IsTwoWay { get; set; }
        public short? FlightClassID { get; set; }
        public long? AirportID { get; set; }
        public long? ReturnAirportID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Rate { get; set; }
        public long? DepartmentID { get; set; }
        [StringLength(50)]
        public string GenerateTravelSector { get; set; }
        [ForeignKey("AirportID")]
        [InverseProperty("SectorTicketAirfareAirports")]
        public virtual Airport Airport { get; set; }
        [ForeignKey("DepartmentID")]
        [InverseProperty("SectorTicketAirfares")]
        public virtual Eduegate.Domain.Entity.HR.Models.Departments1 Department { get; set; }
        [ForeignKey("ReturnAirportID")]
        [InverseProperty("SectorTicketAirfareReturnAirports")]
        public virtual Airport ReturnAirport { get; set; }

        [ForeignKey("FlightClassID")]
        [InverseProperty("SectorTicketAirfares")]
        public virtual FlightClass FlightClass { get; set; }
    }
}
