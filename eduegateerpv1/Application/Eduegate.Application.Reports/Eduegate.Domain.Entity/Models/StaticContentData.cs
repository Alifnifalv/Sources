using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class StaticContentData
    {
        public long ContentDataIID { get; set; }
        public Nullable<int> ContentTypeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageFilePath { get; set; }
        public string SerializedJsonParameters { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual StaticContentType StaticContentType { get; set; }
    }
}
