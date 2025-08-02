using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Settings", Schema = "setting")]
    public partial class Setting
    {
        [Key]
        public string SettingCode { get; set; }
        [Key]
        public int CompanyID { get; set; }
        public string GroupName { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string ValueType { get; set; }
        public int? LookupTypeID { get; set; }
    }
}
