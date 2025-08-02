using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BranchView
    {
        public long BranchIID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        public byte? StatusID { get; set; }
        public bool? IsMarketPlace { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        public int? CompanyID { get; set; }
        public long? supplieriid { get; set; }
        [StringLength(767)]
        public string Supplier { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
