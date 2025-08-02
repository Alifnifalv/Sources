using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class StaticContentType
    {
        public StaticContentType()
        {
            this.StaticContentDatas = new List<StaticContentData>();
        }

        public int ContentTypeID { get; set; }
        public string ContentTypeName { get; set; }
        public string Description { get; set; }
        public string ContentTemplateFilePath { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<StaticContentData> StaticContentDatas { get; set; }
    }
}
