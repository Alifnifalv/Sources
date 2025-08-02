using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("IndustryTypes", Schema = "crm")]
    public partial class IndustryType
    {
        public IndustryType()
        {
            Leads = new HashSet<Lead>();
        }

        [Key]
        public int IndustryTypeID { get; set; }
        [StringLength(50)]
        public string IndustryTypeName { get; set; }

        [InverseProperty("IndustryType")]
        public virtual ICollection<Lead> Leads { get; set; }
    }
}
