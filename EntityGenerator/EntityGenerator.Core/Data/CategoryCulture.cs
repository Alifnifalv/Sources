using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CategoryCulture
    {
        public long CategoryIID { get; set; }
        public long? ParentCategoryID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CategoryCode { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
        [StringLength(500)]
        public string ImageName { get; set; }
        [StringLength(500)]
        public string ThumbnailImageName { get; set; }
        public long? SeoMetadataID { get; set; }
        public bool? IsInNavigationMenu { get; set; }
        public bool? IsActive { get; set; }
        public byte? CultureID { get; set; }
    }
}
