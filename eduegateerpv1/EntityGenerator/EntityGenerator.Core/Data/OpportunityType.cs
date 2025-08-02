using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OpportunityTypes", Schema = "crm")]
    public partial class OpportunityType
    {
        public OpportunityType()
        {
            Opportunities = new HashSet<Opportunity>();
        }

        [Key]
        public int OpportunityTypeID { get; set; }
        [StringLength(50)]
        public string OpportunityTypeName { get; set; }

        [InverseProperty("OpportunityType")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}
