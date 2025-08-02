using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Sources", Schema = "crm")]
    public partial class Source
    {
        public Source()
        {
            Leads = new HashSet<Lead>();
            Opportunities = new HashSet<Opportunity>();
        }

        [Key]
        public int SourceID { get; set; }
        [StringLength(50)]
        public string SourceName { get; set; }

        [InverseProperty("LeadSource")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("Sources")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}
