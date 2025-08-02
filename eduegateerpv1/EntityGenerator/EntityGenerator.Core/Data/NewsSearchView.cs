using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class NewsSearchView
    {
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        public long NewsIID { get; set; }
        [StringLength(500)]
        public string NewsContentShort { get; set; }
        public string NewsContent { get; set; }
        public int? NewsTypeID { get; set; }
        [StringLength(500)]
        public string ThumbnailUrl { get; set; }
        public int? CreatedBy { get; set; }
        public int? companyID { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(50)]
        public string NewsTypeName { get; set; }
    }
}
