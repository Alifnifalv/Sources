using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class JobOperationSummaryView
    {
        public int? Route1 { get; set; }
        public int? Route2 { get; set; }
        public int? Route3 { get; set; }
        public int? Route4 { get; set; }
        public int? OnlineOrders { get; set; }
        public int? OfflineOrders { get; set; }
    }
}
