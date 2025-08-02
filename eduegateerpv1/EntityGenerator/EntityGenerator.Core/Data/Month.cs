using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Month")]
    public partial class Month
    {
        public byte MonthID { get; set; }
        [StringLength(15)]
        public string Description { get; set; }
    }
}
