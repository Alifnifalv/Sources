using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarkStatuses", Schema = "schools")]
    public partial class MarkStatus
    {
        [Key]
        public int MarkStatusID { get; set; }
        [StringLength(255)]
        public string StatusName { get; set; }
        [StringLength(255)]
        public string StatusCode { get; set; }
        public bool? IsActive { get; set; }
    }
}
