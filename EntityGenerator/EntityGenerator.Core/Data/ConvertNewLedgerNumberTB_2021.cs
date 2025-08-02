using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ConvertNewLedgerNumberTB_2021
    {
        [Column("GL NAME")]
        [StringLength(255)]
        public string GL_NAME { get; set; }
        public string SearchData { get; set; }
        public long? RowIndex { get; set; }
    }
}
