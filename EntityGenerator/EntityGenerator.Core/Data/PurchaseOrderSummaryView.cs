using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PurchaseOrderSummaryView
    {
        public int? TotalPurchaseOrders { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
    }
}
