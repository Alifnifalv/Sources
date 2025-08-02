using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Language1
    {
        public Language1()
        {
            this.Companies = new List<Company>();
            this.Countries1 = new List<Countries1>();
        }

        public int LanguageID { get; set; }
        public string Language { get; set; }
        public string LanguageCodeTwoLetter { get; set; }
        public string LanguageCodeThreeLetter { get; set; }
        public string CultureCode { get; set; }
        public Nullable<long> CountryID { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Countries1> Countries1 { get; set; }
    }
}
