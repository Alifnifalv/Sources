using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class OrderSummaryView
    {
        public int? TotalOrders { get; set; }
        public int? MonthOrders { get; set; }
        public int? TodaysOrders { get; set; }
    }
}
