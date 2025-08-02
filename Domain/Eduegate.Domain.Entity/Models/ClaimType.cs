using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ClaimTypes", Schema = "admin")]
    public partial class ClaimType
    {
        public ClaimType()
        {
            this.Claims = new List<Claim>();
        }

        [Key]
        public int ClaimTypeID { get; set; }
        public string ClaimTypeName { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
    }
}
