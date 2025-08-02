using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AdditionalExpenses", Schema = "account")]
    public partial class AdditionalExpens
    {
        [Key]
        public int AdditionalExpenseID { get; set; }
        [StringLength(50)]
        public string AdditionalExpenseCode { get; set; }
        [StringLength(100)]
        public string AdditionalExpenseName { get; set; }
        public long? AccountID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? CompanyID { get; set; }
    }
}
