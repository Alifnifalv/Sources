using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CompanyCurrencyMaps", Schema = "mutual")]
    public partial class CompanyCurrencyMap
    {
        [Key]
        public int CompanyID { get; set; }
        [Key]
        public int CurrencyID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("CompanyCurrencyMaps")]
        public virtual Company Company { get; set; }
        [ForeignKey("CurrencyID")]
        [InverseProperty("CompanyCurrencyMaps")]
        public virtual Currency Currency { get; set; }
    }
}
