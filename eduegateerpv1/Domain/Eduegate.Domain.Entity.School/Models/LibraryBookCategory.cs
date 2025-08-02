namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LibraryBookCategories", Schema = "schools")]
    public partial class LibraryBookCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LibraryBookCategory()
        {
            LibraryBookCategoryMaps = new HashSet<LibraryBookCategoryMap>();
            LibraryBooks = new HashSet<LibraryBook>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long LibraryBookCategoryID { get; set; }

        [StringLength(50)]
        public string BookCategoryName { get; set; }
        public string CategoryCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryBookCategoryMap> LibraryBookCategoryMaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
