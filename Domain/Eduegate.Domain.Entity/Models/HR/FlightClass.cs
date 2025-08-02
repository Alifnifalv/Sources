using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;
namespace Eduegate.Domain.Entity.Models.HR
{
    [Table("FlightClasses", Schema = "mutual")]
    public partial class FlightClass
    {
        public FlightClass()
        {
            Employees = new HashSet<Employee>();
            //TicketEntitilementEntries = new HashSet<TicketEntitilementEntry>();
        }

        [Key]
        public short FlightClassID { get; set; }
        [StringLength(100)]
        public string FlightClassName { get; set; }

        [InverseProperty("FlightClass")]
        public virtual ICollection<Employee> Employees { get; set; }
        //[InverseProperty("FlightClass")]
        //public virtual ICollection<TicketEntitilementEntry> TicketEntitilementEntries { get; set; }
    }
}
