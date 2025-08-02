namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LibraryBookCategoryMaps", Schema = "schools")]
    public partial class LibraryBookCategoryMap
    {
        [Key]
        public long LibraryBookCategoryMapIID { get; set; }

        public long? LibraryBookID { get; set; }

        public long? BookCategoryID { get; set; }

        public virtual LibraryBookCategory LibraryBookCategory { get; set; }

        public virtual LibraryBook LibraryBook { get; set; }
    }
}
