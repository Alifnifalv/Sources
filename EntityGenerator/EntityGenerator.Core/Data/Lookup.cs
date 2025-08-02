using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Lookups", Schema = "setting")]
    public partial class Lookup
    {
        [Key]
        public int LookupID { get; set; }
        [StringLength(10)]
        public string LookupType { get; set; }
        [StringLength(50)]
        public string LookupName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Query { get; set; }
        [StringLength(500)]
        public string Value1 { get; set; }
        [StringLength(500)]
        public string Value2 { get; set; }
    }
}
