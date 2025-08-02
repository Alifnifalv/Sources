using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SuppliersC2
    {
        public int? Account_No { get; set; }
        [StringLength(50)]
        public string Account_Name { get; set; }
        public double? Opening_Balance { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public double? Closing_Balance { get; set; }
        public int? AccountID { get; set; }
    }
}
