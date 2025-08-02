using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OpportunityStatuses", Schema = "crm")]
    public partial class OpportunityStatus
    {
        public OpportunityStatus()
        {
            Opportunities = new HashSet<Opportunity>();
        }

        [Key]
        public byte OpportunityStatusID { get; set; }
        [StringLength(50)]
        public string OpportunityStatusName { get; set; }

        [InverseProperty("OpportunityStatus")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}
