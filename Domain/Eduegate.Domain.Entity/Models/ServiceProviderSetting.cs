using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceProviderSetting
    {
        [Key]
        public int ServiceProviderID { get; set; }
        public string SettingCode { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
    }
}
