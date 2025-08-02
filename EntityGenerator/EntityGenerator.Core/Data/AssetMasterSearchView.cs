using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AssetMasterSearchView
    {
        [StringLength(50)]
        public string AssetGlAccName { get; set; }
        [StringLength(50)]
        public string AccumulatedDepGLAccName { get; set; }
        [StringLength(50)]
        public string DepreciationExpGLAccName { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Alias { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Alias1 { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Alias2 { get; set; }
        public long AssetCategoryID { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string CategoryName { get; set; }
        public long AssetIID { get; set; }
        [StringLength(50)]
        public string AssetCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public long? AssetGlAccID { get; set; }
        public long? AccumulatedDepGLAccID { get; set; }
        public long? DepreciationExpGLAccId { get; set; }
        public int? DepreciationYears { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
