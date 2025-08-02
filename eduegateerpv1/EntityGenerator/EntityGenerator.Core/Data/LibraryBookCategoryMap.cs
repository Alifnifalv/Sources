using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryBookCategoryMaps", Schema = "schools")]
    public partial class LibraryBookCategoryMap
    {
        [Key]
        public long LibraryBookCategoryMapIID { get; set; }
        public long? LibraryBookID { get; set; }
        public long? BookCategoryID { get; set; }

        [ForeignKey("BookCategoryID")]
        [InverseProperty("LibraryBookCategoryMaps")]
        public virtual LibraryBookCategory BookCategory { get; set; }
        [ForeignKey("LibraryBookID")]
        [InverseProperty("LibraryBookCategoryMaps")]
        public virtual LibraryBook LibraryBook { get; set; }
    }
}
