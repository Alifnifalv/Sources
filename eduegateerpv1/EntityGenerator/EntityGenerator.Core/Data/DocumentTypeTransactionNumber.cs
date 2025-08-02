using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentTypeTransactionNumbers", Schema = "mutual")]
    public partial class DocumentTypeTransactionNumber
    {
        [Key]
        public int DocumentTypeID { get; set; }
        [Key]
        public int Year { get; set; }
        [Key]
        public int Month { get; set; }
        public long? LastTransactionNo { get; set; }

        [ForeignKey("DocumentTypeID")]
        [InverseProperty("DocumentTypeTransactionNumbers")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
