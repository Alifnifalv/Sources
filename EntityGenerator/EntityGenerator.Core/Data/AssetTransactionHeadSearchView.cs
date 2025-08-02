using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AssetTransactionHeadSearchView
    {
        public long HeadIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        public long? DocumentStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        public byte? ProcessingStatusID { get; set; }
        [StringLength(50)]
        public string TransactionStatusesName { get; set; }
        [Required]
        public string AssetCode { get; set; }
    }
}
