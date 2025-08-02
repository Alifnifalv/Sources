using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataFeedLogDetails", Schema = "feed")]
    public partial class DataFeedLogDetail
    {
        [Key]
        public long DataFeedLogDetailIID { get; set; }
        public Nullable<long> DataFeedLogID { get; set; }
        public string ErrorMessage { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
