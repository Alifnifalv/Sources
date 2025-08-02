using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Models.HR;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Airports", Schema = "mutual")]
    public partial class Airport
    {
        public Airport()
        {
            TicketEntitilements = new HashSet<TicketEntitilement>();
            EmployeeEmployeeCountryAirports = new HashSet<Employee>();
            EmployeeEmployeeNearestAirports = new HashSet<Employee>();
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
        public virtual ICollection<TicketEntitilement> TicketEntitilements { get; set; }
        public virtual ICollection<Employee> EmployeeEmployeeCountryAirports { get; set; }
        public virtual ICollection<Employee> EmployeeEmployeeNearestAirports { get; set; }
    }
}
