using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Taxes", Schema = "inventory")]
    public partial class Tax
    {
        [Key]
        public long TaxID { get; set; }
        [StringLength(50)]
        public string TaxName { get; set; }
        public int? TaxTypeID { get; set; }
        public long? AccountID { get; set; }
        public int? Percentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int? TaxStatusID { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("Taxes")]
        public virtual Account Account { get; set; }
        [ForeignKey("TaxStatusID")]
        [InverseProperty("Taxes")]
        public virtual TaxStatus TaxStatus { get; set; }
        [ForeignKey("TaxTypeID")]
        [InverseProperty("Taxes")]
        public virtual TaxType TaxType { get; set; }
    }
}
