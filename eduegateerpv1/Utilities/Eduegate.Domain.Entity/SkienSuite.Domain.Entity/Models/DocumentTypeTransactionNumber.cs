using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DocumentTypeTransactionNumber
    {
        public int DocumentTypeID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Nullable<long> LastTransactionNo { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}
