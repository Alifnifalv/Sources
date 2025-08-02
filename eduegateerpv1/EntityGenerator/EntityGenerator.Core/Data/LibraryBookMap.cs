using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryBookMaps", Schema = "schools")]
    public partial class LibraryBookMap
    {
        public LibraryBookMap()
        {
            LibraryTransactions = new HashSet<LibraryTransaction>();
        }

        [Key]
        public int LibraryBookMapIID { get; set; }
        public long? LibraryBookID { get; set; }
        [StringLength(100)]
        public string Call_No { get; set; }
        [StringLength(100)]
        public string Acc_No { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LibraryBookID")]
        [InverseProperty("LibraryBookMaps")]
        public virtual LibraryBook LibraryBook { get; set; }
        [InverseProperty("LibraryBookMap")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
    }
}
