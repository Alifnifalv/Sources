using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OpportunityFroms", Schema = "crm")]
    public partial class OpportunityFrom
    {
        public OpportunityFrom()
        {
            Opportunities = new HashSet<Opportunity>();
        }

        [Key]
        public byte OpportunityFromID { get; set; }
        [StringLength(50)]
        public string OpportunityFromName { get; set; }

        [InverseProperty("OpportunityFrom")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}
