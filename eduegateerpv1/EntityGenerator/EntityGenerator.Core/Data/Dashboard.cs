using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Dashboards", Schema = "setting")]
    public partial class Dashboard
    {
        [Key]
        public long Dsh_ID { get; set; }
        public long? MenuLinkID { get; set; }
        public long? PageID { get; set; }
        [StringLength(255)]
        public string ReportID { get; set; }
        [StringLength(255)]
        public string WorkspaceID { get; set; }
        [StringLength(255)]
        public string TenantID { get; set; }
        [StringLength(255)]
        public string ClientID { get; set; }
        [StringLength(255)]
        public string ClientSecret { get; set; }
        [StringLength(255)]
        public string ClintToken { get; set; }
        [StringLength(255)]
        public string EmbededLink { get; set; }
    }
}
