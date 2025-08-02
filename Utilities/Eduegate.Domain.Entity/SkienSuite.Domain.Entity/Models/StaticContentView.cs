using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class StaticContentView
    {
        public long ContentDataIID { get; set; }
        public Nullable<int> ContentTypeID { get; set; }
        public string ContentTypeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SerializedJsonParameters { get; set; }
        public string ImageFilePath { get; set; }
    }
}
