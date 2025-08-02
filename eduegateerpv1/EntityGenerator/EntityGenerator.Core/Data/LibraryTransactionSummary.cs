using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LibraryTransactionSummary
    {
        public byte? SchoolID { get; set; }
        public int? TotalBookCount { get; set; }
        public int? IssuedBooks { get; set; }
    }
}
