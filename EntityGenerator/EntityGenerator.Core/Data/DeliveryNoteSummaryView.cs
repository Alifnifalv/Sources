using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DeliveryNoteSummaryView
    {
        public int? TotalSalesInvoice { get; set; }
        public int? TodaysSales { get; set; }
        public int? YesterdaysSales { get; set; }
    }
}
