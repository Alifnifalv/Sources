using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStatusBatchLog
    {
        [Key]
        public int ProductStockBatchLogID { get; set; }
        public long BatchNo { get; set; }
        public int RecordsUploaded { get; set; }
        public int RecordsUpdated { get; set; }
        public long CreatedByID { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
