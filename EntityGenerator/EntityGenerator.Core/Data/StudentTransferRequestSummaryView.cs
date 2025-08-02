using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentTransferRequestSummaryView
    {
        public int? TotalRequest { get; set; }
        public int? Requested { get; set; }
        public int? Processing { get; set; }
        public int? Completed { get; set; }
    }
}
