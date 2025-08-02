using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Accounts_Reference", Schema = "account")]
    public partial class Accounts_Reference
    {
        [Key]
        public int Ref_ID { get; set; }
        [Required]
        [StringLength(2500)]
        [Unicode(false)]
        public string Ref_No { get; set; }
        [Required]
        [StringLength(2500)]
        [Unicode(false)]
        public string Ref_No1 { get; set; }
        [Required]
        [StringLength(2500)]
        [Unicode(false)]
        public string Ref_No2 { get; set; }
    }
}
