using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDisableQueryBatchLog
    {
        [Key]
        public int ProductDisableQueryBatchLogID { get; set; }
        public long BatchNo { get; set; }
        public int NoOfDays { get; set; }
        public int RecordsUpdated { get; set; }
        public long CreatedByID { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
