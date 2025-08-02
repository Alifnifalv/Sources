using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CRMCompanies", Schema = "crm")]
    public partial class CRMCompany
    {
        public CRMCompany()
        {
            Leads = new HashSet<Lead>();
            Opportunities = new HashSet<Opportunity>();
        }

        [Key]
        public int CompanyID { get; set; }
        [StringLength(50)]
        public string CompanyName { get; set; }

        [InverseProperty("Company")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}
