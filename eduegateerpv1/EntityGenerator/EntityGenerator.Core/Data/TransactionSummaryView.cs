using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TransactionSummaryView
    {
        public int? Total { get; set; }
        public int? TotalSalesOrders { get; set; }
        public int? TotalSalesInvoice { get; set; }
    }
}
