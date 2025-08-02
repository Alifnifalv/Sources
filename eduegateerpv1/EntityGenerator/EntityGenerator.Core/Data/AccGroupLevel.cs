using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("AccGroupLevel", Schema = "account")]
    public partial class AccGroupLevel
    {
        public long? Main_GroupID { get; set; }
        [StringLength(50)]
        public string Main_GroupCode { get; set; }
        [StringLength(100)]
        public string Main_GroupName { get; set; }
        public long? Sub_GroupID { get; set; }
        [StringLength(50)]
        public string Sub_GroupCode { get; set; }
        [StringLength(100)]
        public string Sub_GroupName { get; set; }
        public long? GroupID { get; set; }
        [StringLength(50)]
        public string GroupCode { get; set; }
        [StringLength(100)]
        public string GroupName { get; set; }
    }
}
