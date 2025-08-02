using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("Countries", Schema = "mutual")]
    public partial class Country
    {
        public Country()
        {
            Companies = new HashSet<Company>();
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
        public int CountryID { get; set; }

        [StringLength(50)]
        public string CountryName { get; set; }

        [StringLength(2)]
        [Unicode(false)]
        public string TwoLetterCode { get; set; }

        [StringLength(3)]
        [Unicode(false)]
        public string ThreeLetterCode { get; set; }

        public int? CurrencyID { get; set; }

        public int? LanguageID { get; set; }

        [StringLength(20)]
        public string CountryCode { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}