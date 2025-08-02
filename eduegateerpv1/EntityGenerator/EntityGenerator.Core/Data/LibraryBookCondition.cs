using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryBookConditions", Schema = "schools")]
    public partial class LibraryBookCondition
    {
        public LibraryBookCondition()
        {
            LibraryBooks = new HashSet<LibraryBook>();
            LibraryTransactions = new HashSet<LibraryTransaction>();
        }

        [Key]
        public byte BookConditionID { get; set; }
        [StringLength(50)]
        public string BookConditionName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        [InverseProperty("BookCondition")]
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
        [InverseProperty("BookCondion")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
    }
}
