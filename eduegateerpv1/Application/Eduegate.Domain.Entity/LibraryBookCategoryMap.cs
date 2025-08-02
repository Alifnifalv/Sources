namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.LibraryBookCategoryMaps")]
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
