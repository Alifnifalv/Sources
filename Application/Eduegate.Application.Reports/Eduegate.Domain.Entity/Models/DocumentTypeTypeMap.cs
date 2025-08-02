using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DocumentTypeTypeMap
    {
        public long DocumentTypeTypeMapIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<int> DocumentTypeMapID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual DocumentType DocumentType1 { get; set; }
    }
}
