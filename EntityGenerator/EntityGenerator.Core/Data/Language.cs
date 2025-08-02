using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Languages", Schema = "mutual")]
    public partial class Language
    {
        public Language()
        {
            StudentApplicationSecoundLanguages = new HashSet<StudentApplication>();
            StudentApplicationThridLanguages = new HashSet<StudentApplication>();
            StudentSecoundLanguages = new HashSet<Student>();
            StudentThridLanguages = new HashSet<Student>();
        }

        [Key]
        public int LanguageIID { get; set; }
        [StringLength(50)]
        public string LanguageName { get; set; }
        [StringLength(50)]
        public string LanguageCode { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(2)]
        public string LanguageCodeTwoLetter { get; set; }
        [StringLength(3)]
        public string LanguageCodeThreeLetter { get; set; }
        public int? CultureID { get; set; }
        public bool? IsEnabled { get; set; }
        public int? CountryID { get; set; }
        public int? LanguageID { get; set; }
        [Column("Language")]
        [StringLength(50)]
        public string Language1 { get; set; }

        [InverseProperty("SecoundLanguage")]
        public virtual ICollection<StudentApplication> StudentApplicationSecoundLanguages { get; set; }
        [InverseProperty("ThridLanguage")]
        public virtual ICollection<StudentApplication> StudentApplicationThridLanguages { get; set; }
        [InverseProperty("SecoundLanguage")]
        public virtual ICollection<Student> StudentSecoundLanguages { get; set; }
        [InverseProperty("ThridLanguage")]
        public virtual ICollection<Student> StudentThridLanguages { get; set; }
    }
}
