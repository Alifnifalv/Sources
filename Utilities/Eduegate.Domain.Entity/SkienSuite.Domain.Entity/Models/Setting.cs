using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Setting
    {
        public string SettingCode { get; set; }
        public int CompanyID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
        public Nullable<bool> ShowProductImageForPOS { get; set; }
        public string GroupName { get; set; }
        public virtual Company Company { get; set; }
    }
}
