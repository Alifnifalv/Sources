using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryBookStatuses", Schema = "schools")]
    public partial class LibraryBookStatus
    {
        public LibraryBookStatus()
        {
            LibraryBooks = new HashSet<LibraryBook>();
        }

        [Key]
        public byte LibraryBookStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("BookStatus")]
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
