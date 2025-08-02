using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_CHART_OF_ACCOUNTS
    {
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string GL { get; set; }
        public int? Lvl { get; set; }
        public long? GroupID { get; set; }
        public int? Parent_ID { get; set; }
        [Unicode(false)]
        public string Lvl_Sort { get; set; }
        [StringLength(50)]
        public string GroupCode { get; set; }
        [StringLength(100)]
        public string GroupName { get; set; }
        [StringLength(50)]
        public string PartCode { get; set; }
        [StringLength(500)]
        public string Particulars { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string AccountCode { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Default_Side { get; set; }
        public long AccountID { get; set; }
    }
}
