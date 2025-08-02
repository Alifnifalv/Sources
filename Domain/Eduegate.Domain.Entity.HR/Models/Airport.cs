using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.HR.Payroll;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("Airports", Schema = "mutual")]
    public partial class Airport
    {
        public Airport()
        {            
            TicketEntitilements = new HashSet<TicketEntitilement>();
            EmployeeEmployeeCountryAirports = new HashSet<Employee>();
            EmployeeEmployeeNearestAirports = new HashSet<Employee>();
            SectorTicketAirfareAirports = new HashSet<SectorTicketAirfare>();
            SectorTicketAirfareReturnAirports = new HashSet<SectorTicketAirfare>();
            TicketEntitilementEntries = new HashSet<TicketEntitilementEntry>();
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

        public virtual Country Country { get; set; }

        public virtual ICollection<TicketEntitilement> TicketEntitilements { get; set; }
        public virtual ICollection<Employee> EmployeeEmployeeCountryAirports { get; set; }  
        public virtual ICollection<Employee> EmployeeEmployeeNearestAirports { get; set; }
        public virtual ICollection<SectorTicketAirfare> SectorTicketAirfareAirports { get; set; }        
        public virtual ICollection<SectorTicketAirfare> SectorTicketAirfareReturnAirports { get; set; }
        public virtual ICollection<TicketEntitilementEntry> TicketEntitilementEntries { get; set; }

    }
}