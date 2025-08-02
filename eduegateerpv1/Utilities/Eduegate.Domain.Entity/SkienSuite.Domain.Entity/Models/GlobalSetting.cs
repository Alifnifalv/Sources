using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class GlobalSetting
    {
        public int GlobalSettingIID { get; set; }
        public string GlobalSettingKey { get; set; }
        public string GlobalSettingValue { get; set; }
    }
}
