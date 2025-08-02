using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceProviderSetting
    {
        public int ServiceProviderID { get; set; }
        public string SettingCode { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
    }
}
