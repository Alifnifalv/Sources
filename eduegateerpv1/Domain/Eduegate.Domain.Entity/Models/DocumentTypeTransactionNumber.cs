using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DocumentTypeTransactionNumbers", Schema = "mutual")]
    public partial class DocumentTypeTransactionNumber
    {
        [Key]
        public int DocumentTypeID { get; set; }
        public int Year { get; set; }

        public int Month { get; set; }
        public Nullable<long> LastTransactionNo { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}
