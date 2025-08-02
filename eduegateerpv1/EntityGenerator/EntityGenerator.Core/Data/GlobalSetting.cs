using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("GlobalSettings", Schema = "setting")]
    public partial class GlobalSetting
    {
        [Key]
        public int GlobalSettingIID { get; set; }
        [StringLength(300)]
        [Unicode(false)]
        public string GlobalSettingKey { get; set; }
        [StringLength(300)]
        public string GlobalSettingValue { get; set; }
    }
}
