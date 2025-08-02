namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.CompanyCurrencyMaps")]
    public partial class CompanyCurrencyMap
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CurrencyID { get; set; }

        public decimal? ExchangeRate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Company Company { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
