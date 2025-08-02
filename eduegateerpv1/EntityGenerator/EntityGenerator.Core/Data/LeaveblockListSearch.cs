using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LeaveblockListSearch
    {
        public long LeaveBlockListIID { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
