using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("LibrarybookmapsExcel")]
    public partial class LibrarybookmapsExcel
    {
        public double? LibraryBookMapIID { get; set; }
        public double? LibraryBookID { get; set; }
        [StringLength(255)]
        public string Call_No { get; set; }
        [StringLength(255)]
        public string Acc_No { get; set; }
    }
}
