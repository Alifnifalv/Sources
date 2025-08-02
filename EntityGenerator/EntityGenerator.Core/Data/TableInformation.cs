using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TableInformation
    {
        [Required]
        [StringLength(257)]
        public string TableName { get; set; }
        public long? TotalSpaceKB { get; set; }
        public int IsForArchive { get; set; }
    }
}
