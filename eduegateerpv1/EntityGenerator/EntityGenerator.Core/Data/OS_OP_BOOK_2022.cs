using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class OS_OP_BOOK_2022
    {
        public short StudentIID { get; set; }
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
    }
}
