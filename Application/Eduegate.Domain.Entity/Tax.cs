namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.Taxes")]
    public partial class Tax
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TaxID { get; set; }

        [StringLength(50)]
        public string TaxName { get; set; }

        public int? TaxTypeID { get; set; }

        public long? AccountID { get; set; }

        public int? Percentage { get; set; }

        public decimal? Amount { get; set; }

        public int? TaxStatusID { get; set; }

        public virtual Account Account { get; set; }

        public virtual TaxStatus TaxStatus { get; set; }

        public virtual TaxType TaxType { get; set; }
    }
}
