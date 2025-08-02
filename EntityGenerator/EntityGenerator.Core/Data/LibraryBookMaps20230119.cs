using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LibraryBookMaps20230119
    {
        public int LibraryBookMapIID { get; set; }
        public long? LibraryBookID { get; set; }
        [StringLength(100)]
        public string Call_No { get; set; }
        [StringLength(100)]
        public string Acc_No { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
