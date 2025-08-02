using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Settings", Schema = "setting")]
    [Index("SiteID", "LookupTypeID", Name = "idx_SettingsSiteId")]
    public partial class Setting
    {
        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string SettingCode { get; set; }
        [Key]
        public int CompanyID { get; set; }
        public int? SiteID { get; set; }
        [StringLength(1000)]
        public string SettingValue { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public bool? ShowProductImageForPOS { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ValueType { get; set; }
        public int? LookupTypeID { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("Settings")]
        public virtual Company Company { get; set; }
    }
}
