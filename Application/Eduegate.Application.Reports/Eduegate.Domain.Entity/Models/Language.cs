using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Language 
    {
        public Language()
        {
            this.Companies = new List<Company>();
        }

        public int LanguageID { get; set; }
        public string Language1 { get; set; }
        public string LanguageCodeTwoLetter { get; set; }
        public string LanguageCodeThreeLetter { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual Country Country { get; set; }
        public Nullable<byte> CultureID { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
