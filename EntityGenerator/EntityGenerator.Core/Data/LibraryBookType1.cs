using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryBookTypes", Schema = "schools")]
    public partial class LibraryBookType1
    {
        public LibraryBookType1()
        {
            LibraryBooks = new HashSet<LibraryBook>();
        }

        [Key]
        public byte LibraryBookTypeID { get; set; }
        [StringLength(50)]
        public string LibraryBookName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        [InverseProperty("LibraryBookType")]
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
