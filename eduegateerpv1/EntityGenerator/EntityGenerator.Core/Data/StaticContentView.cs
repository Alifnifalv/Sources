using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StaticContentView
    {
        public long ContentDataIID { get; set; }
        public int? ContentTypeID { get; set; }
        [StringLength(50)]
        public string ContentTypeName { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string SerializedJsonParameters { get; set; }
        [StringLength(250)]
        public string ImageFilePath { get; set; }
    }
}
