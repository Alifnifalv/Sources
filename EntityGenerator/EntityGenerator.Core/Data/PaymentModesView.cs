using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PaymentModesView
    {
        public int PaymentModeID { get; set; }
        [StringLength(50)]
        public string PaymentModeName { get; set; }
        public long? AccountId { get; set; }
        [StringLength(50)]
        public string Account { get; set; }
        public int? TenderTypeID { get; set; }
        [StringLength(50)]
        public string TenderType { get; set; }
    }
}
