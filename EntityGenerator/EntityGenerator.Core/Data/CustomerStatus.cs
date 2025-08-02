using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerStatuses", Schema = "mutual")]
    public partial class CustomerStatus
    {
        [Key]
        public byte CustomerStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
