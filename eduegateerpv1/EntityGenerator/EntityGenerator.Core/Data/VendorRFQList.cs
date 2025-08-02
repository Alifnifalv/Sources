using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VendorRFQList
    {
        public long HeadIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        public long? SupplierID { get; set; }
        public byte? SchoolID { get; set; }
        public long? LoginID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public long? CreatedBy { get; set; }
        public long? DocumentStatusID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string QTNumber { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string Status { get; set; }
    }
}
