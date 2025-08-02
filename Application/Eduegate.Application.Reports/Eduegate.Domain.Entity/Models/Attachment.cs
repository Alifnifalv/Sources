using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Attachment
    {
        public Attachment()
        {
        }

        public long AttachmentIID { get; set; }
        public int EntityTypeID { get; set; }
        public long ReferenceID { get; set; }
        public string AttachmentName { get; set; }
        public string FileName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public virtual EntityType EntityType { get; set; }
    }
}
