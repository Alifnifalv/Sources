using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeadTypes", Schema = "crm")]
    public partial class LeadType
    {
        public LeadType()
        {
            Leads = new HashSet<Lead>();
        }

        [Key]
        public byte LeadTypeID { get; set; }
        [StringLength(50)]
        public string LeadTypeName { get; set; }

        [InverseProperty("LeadType")]
        public virtual ICollection<Lead> Leads { get; set; }
    }
}
