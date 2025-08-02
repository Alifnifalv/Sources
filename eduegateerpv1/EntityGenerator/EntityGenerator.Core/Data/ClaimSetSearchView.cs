using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClaimSetSearchView
    {
        public long ClaimSetIID { get; set; }
        [StringLength(50)]
        public string ClaimSetName { get; set; }
        public int NoOfClaimSets { get; set; }
        public int NoOfClaims { get; set; }
        public int? CompanyID { get; set; }
    }
}
