namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Languages")]
    public partial class Language
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Language()
        {
            StudentApplications = new HashSet<StudentApplication>();
            StudentApplications1 = new HashSet<StudentApplication>();
            Students = new HashSet<Student>();
            Students1 = new HashSet<Student>();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplication> StudentApplications1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students1 { get; set; }
    }
}
