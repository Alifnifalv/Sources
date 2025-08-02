using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeadStatuses", Schema = "crm")]
    public partial class LeadStatus
    {
        public LeadStatus()
        {
            Leads = new HashSet<Lead>();
        }

        [Key]
        public byte LeadStatusID { get; set; }
        [StringLength(50)]
        public string LeadStatusName { get; set; }

        [InverseProperty("LeadStatus")]
        public virtual ICollection<Lead> Leads { get; set; }
    }
}
