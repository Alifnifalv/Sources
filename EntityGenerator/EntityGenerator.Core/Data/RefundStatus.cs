using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RefundStatuses", Schema = "cs")]
    public partial class RefundStatus
    {
        [Key]
        public int RefundStatusID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string RefundStatusName { get; set; }
    }
}
