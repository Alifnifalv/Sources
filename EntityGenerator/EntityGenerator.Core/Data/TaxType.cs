using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaxTypes", Schema = "inventory")]
    public partial class TaxType
    {
        public TaxType()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            TaxTemplateItems = new HashSet<TaxTemplateItem>();
            TaxTransactions = new HashSet<TaxTransaction>();
            Taxes = new HashSet<Tax>();
        }

        [Key]
        public int TaxTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("TaxType")]
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        [InverseProperty("TaxType")]
        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }
        [InverseProperty("TaxType")]
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        [InverseProperty("TaxType")]
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
