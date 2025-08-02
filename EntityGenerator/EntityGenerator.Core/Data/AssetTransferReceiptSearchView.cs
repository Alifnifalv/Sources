using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AssetTransferReceiptSearchView
    {
        public long HeadIID { get; set; }
        [StringLength(50)]
        public string TransactionNo { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string FromBranch { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string ToBranch { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? CompanyID { get; set; }
        public int CommentCounts { get; set; }
    }
}
