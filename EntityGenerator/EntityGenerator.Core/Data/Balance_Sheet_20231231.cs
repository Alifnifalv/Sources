using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Balance_Sheet_20231231
    {
        public int? CompanyID { get; set; }
        public int? AccountID { get; set; }
        public int? Period_ID { get; set; }
        public DateTime? SDate { get; set; }
        public DateTime? EDate { get; set; }
        public DateTime? FDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "money")]
        public decimal? OP_Balance { get; set; }
        [Column(TypeName = "money")]
        public decimal? CL_Balance { get; set; }
    }
}
