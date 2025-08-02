using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ClaimType
    {
        public ClaimType()
        {
            this.Claims = new List<Claim>();
        }

        public int ClaimTypeID { get; set; }
        public string ClaimTypeName { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
    }
}
