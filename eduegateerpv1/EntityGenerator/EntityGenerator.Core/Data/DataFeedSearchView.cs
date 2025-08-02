using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DataFeedSearchView
    {
        public long DataFeedLogIID { get; set; }
        [StringLength(250)]
        public string FileName { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string FeedName { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(510)]
        public string ImportedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? companyID { get; set; }
    }
}
