using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RefundTypes", Schema = "cs")]
    public partial class RefundType
    {
        [Key]
        public int RefundTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string RefundTypeName { get; set; }
    }
}
