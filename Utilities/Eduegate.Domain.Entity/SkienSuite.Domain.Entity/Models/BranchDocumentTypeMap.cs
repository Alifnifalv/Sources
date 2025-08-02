using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BranchDocumentTypeMap
    {
        public long BranchDocumentTypeMapIID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}
