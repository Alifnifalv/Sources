using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryBookCategories", Schema = "schools")]
    public partial class LibraryBookCategory
    {
        public LibraryBookCategory()
        {
            LibraryBookCategoryMaps = new HashSet<LibraryBookCategoryMap>();
            LibraryBooks = new HashSet<LibraryBook>();
        }

        [Key]
        public long LibraryBookCategoryID { get; set; }
        [StringLength(50)]
        public string BookCategoryName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(30)]
        public string CategoryCode { get; set; }

        [InverseProperty("BookCategory")]
        public virtual ICollection<LibraryBookCategoryMap> LibraryBookCategoryMaps { get; set; }
        [InverseProperty("BookCategoryCode")]
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
