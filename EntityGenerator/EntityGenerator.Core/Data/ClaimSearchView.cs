using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClaimSearchView
    {
        public long ClaimIID { get; set; }
        [StringLength(500)]
        public string ClaimName { get; set; }
        [StringLength(50)]
        public string ResourceName { get; set; }
        [StringLength(50)]
        public string ClaimTypeName { get; set; }
        public int? companyid { get; set; }
    }
}
