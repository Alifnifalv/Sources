using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Language
    {
        public Language()
        {
            this.Companies = new List<Company>();
            this.Countries = new List<Country>();
        }

        public int LanguageID { get; set; }
        public string Language1 { get; set; }
        public string LanguageCodeTwoLetter { get; set; }
        public string LanguageCodeThreeLetter { get; set; }
        public Nullable<byte> CultureID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual Country Country { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
