using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ClaimSearchView
    {
        public long ClaimIID { get; set; }
        public string ClaimName { get; set; }
        public string ResourceName { get; set; }
        public string ClaimTypeName { get; set; }
        public Nullable<int> companyid { get; set; }
    }
}
