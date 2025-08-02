namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feed.DataFeedLogDetails")]
    public partial class DataFeedLogDetail
    {
        [Key]
        public long DataFeedLogDetailIID { get; set; }

        public long? DataFeedLogID { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
