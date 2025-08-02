using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.HR.Models;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("TicketEntitilements", Schema = "payroll")]
    public partial class TicketEntitilement
    {
        public TicketEntitilement()
        {
            Employees = new HashSet<Employee>();
            TicketEntitilementEntries = new HashSet<TicketEntitilementEntry>();
        }

        [Key]
        public int TicketEntitilementID { get; set; }

        [StringLength(100)]
        [Column("TicketEntitilement")]
        public string TicketEntitilement1 { get; set; }

        public long? CountryAirportID { get; set; }

        public int? NoOfDays { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual Airport CountryAirport { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<TicketEntitilementEntry> TicketEntitilementEntries { get; set; }
    }
}