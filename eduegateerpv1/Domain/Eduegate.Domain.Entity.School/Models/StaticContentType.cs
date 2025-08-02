using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StaticContentTypes", Schema = "cms")]
    public partial class StaticContentType
    {
        public StaticContentType()
        {
            this.StaticContentDatas = new List<StaticContentData>();
        }

        [Key]
        public int ContentTypeID { get; set; }
        public string ContentTypeName { get; set; }
        public string Description { get; set; }
        public string ContentTemplateFilePath { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<StaticContentData> StaticContentDatas { get; set; }
    }
}
