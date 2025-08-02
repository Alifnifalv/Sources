using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BrandView
    {
        public long BrandIID { get; set; }
        [StringLength(50)]
        public string BrandName { get; set; }
        [StringLength(300)]
        public string LogoFile { get; set; }
        public byte? StatusID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }
        [StringLength(1000)]
        public string Descirption { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(100)]
        public string CreatedUserName { get; set; }
        [StringLength(100)]
        public string UpdatedUserName { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
