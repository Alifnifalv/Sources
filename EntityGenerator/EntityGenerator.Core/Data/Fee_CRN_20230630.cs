using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_CRN_20230630
    {
        public long? FeeDueFeeTypeMapsIID { get; set; }
        public long SchoolCreditNoteIID { get; set; }
        public long CreditNoteFeeTypeMapIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreditNoteDate { get; set; }
        [StringLength(100)]
        public string CreditNoteNumber { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Crn_Amount { get; set; }
        public int EntryType { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string Ref { get; set; }
    }
}
