using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FEE_COLL_NARRATION_20230104
    {
        public long? LedgerAccountID { get; set; }
        public long? OutstandingAccountID { get; set; }
        public long? AdvanceAccountID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? StudentID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? AcadamicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public long FeeCollectionIID { get; set; }
        public long FeeCollectionFeeTypeMapsIID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CollectionDate { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        [Unicode(false)]
        public string Narration { get; set; }
        [Unicode(false)]
        public string Narration1 { get; set; }
        [Unicode(false)]
        public string Narration2 { get; set; }
    }
}
