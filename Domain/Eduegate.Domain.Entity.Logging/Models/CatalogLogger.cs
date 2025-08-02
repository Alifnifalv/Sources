using System;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Logging.Models
{
    public partial class CatalogLogger
    {
        [Key]
        public long CatalogLoggerIID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int OperationTypeID { get; set; }
        public string LogValue { get; set; }
        public string SolrCore { get; set; }
    }
}