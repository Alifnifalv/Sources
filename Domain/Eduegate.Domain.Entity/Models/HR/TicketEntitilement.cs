using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;


namespace Eduegate.Domain.Entity.Models.HR
{
    [Table("TicketEntitilements", Schema = "payroll")]
    public partial class TicketEntitilement
    {
        public TicketEntitilement()
        {
            Employees = new HashSet<Employee>();
            //TicketEntitilementEntries = new HashSet<TicketEntitilementEntry>();
        }

        [Key]
        public int TicketEntitilementID { get; set; }
        [Column("TicketEntitilement")]
        [StringLength(100)]
        public string TicketEntitilement1 { get; set; }
        public long? CountryAirportID { get; set; }
        public int? NoOfDays { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Airport CountryAirport { get; set; }      
        public virtual ICollection<Employee> Employees { get; set; }
       
        //public virtual ICollection<TicketEntitilementEntry> TicketEntitilementEntries { get; set; }
    }
}
