using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BankReconciliationStatus", Schema = "account")]
    public partial class BankReconciliationStatu
    {
        [Key]
        public int StatusID { get; set; }
        [Required]
        [StringLength(255)]
        public string StatusName { get; set; }
    }
}
