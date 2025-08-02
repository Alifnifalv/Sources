using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Airports", Schema = "mutual")]
    public partial class Airport
    {
        public Airport()
        {
            EmployeeEmployeeCountryAirports = new HashSet<Employee>();
            EmployeeEmployeeNearestAirports = new HashSet<Employee>();
            SectorTicketAirfareAirports = new HashSet<SectorTicketAirfare>();
            SectorTicketAirfareReturnAirports = new HashSet<SectorTicketAirfare>();
            TicketEntitilementEntries = new HashSet<TicketEntitilementEntry>();
            TicketEntitilements = new HashSet<TicketEntitilement>();
        }

        [Key]
        public long AirportID { get; set; }
        [StringLength(100)]
        public string AirportName { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string IATA { get; set; }
        public int? CountryID { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("Airports")]
        public virtual Country Country { get; set; }
        [InverseProperty("EmployeeCountryAirport")]
        public virtual ICollection<Employee> EmployeeEmployeeCountryAirports { get; set; }
        [InverseProperty("EmployeeNearestAirport")]
        public virtual ICollection<Employee> EmployeeEmployeeNearestAirports { get; set; }
        [InverseProperty("Airport")]
        public virtual ICollection<SectorTicketAirfare> SectorTicketAirfareAirports { get; set; }
        [InverseProperty("ReturnAirport")]
        public virtual ICollection<SectorTicketAirfare> SectorTicketAirfareReturnAirports { get; set; }
        [InverseProperty("EmployeeCountryAirport")]
        public virtual ICollection<TicketEntitilementEntry> TicketEntitilementEntries { get; set; }
        [InverseProperty("CountryAirport")]
        public virtual ICollection<TicketEntitilement> TicketEntitilements { get; set; }
    }
}
