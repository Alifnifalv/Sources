using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ClaimSetSearchView
    {
        public long ClaimSetIID { get; set; }
        public string ClaimSetName { get; set; }
        public int NoOfClaimSets { get; set; }
        public int NoOfClaims { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
