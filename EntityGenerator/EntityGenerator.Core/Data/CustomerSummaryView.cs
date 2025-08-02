using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CustomerSummaryView
    {
        public int? TotalCustomer { get; set; }
        public int? InActive { get; set; }
    }
}
