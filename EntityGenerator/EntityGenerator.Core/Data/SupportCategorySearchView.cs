using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupportCategorySearchView
    {
        public int SupportCategoryID { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }
        [StringLength(100)]
        public string ParentCategory { get; set; }
        public int? SortOrder { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
