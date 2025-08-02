using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    [Keyless]
    public class Transaction
    {
        public string TransactionTypeName { get; set; }
        public string TransactionNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? Amount { get; set; }
        public long? TransactionCount { get; set; }
    }
}
