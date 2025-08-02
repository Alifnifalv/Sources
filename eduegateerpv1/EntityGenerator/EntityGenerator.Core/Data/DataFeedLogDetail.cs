using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFeedLogDetails", Schema = "feed")]
    public partial class DataFeedLogDetail
    {
        [Key]
        public long DataFeedLogDetailIID { get; set; }
        public long? DataFeedLogID { get; set; }
        public string ErrorMessage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
    }
}
