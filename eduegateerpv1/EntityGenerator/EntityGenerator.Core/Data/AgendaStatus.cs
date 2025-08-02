using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AgendaStatuses", Schema = "schools")]
    public partial class AgendaStatus
    {
        public AgendaStatus()
        {
            Agenda = new HashSet<Agenda>();
        }

        [Key]
        public byte AgendaStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("AgendaStatus")]
        public virtual ICollection<Agenda> Agenda { get; set; }
    }
}
