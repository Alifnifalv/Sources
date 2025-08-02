using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class STUDENT_PRODUCT_CLASS_FEE_RELATION_MAP
    {
        public long? StudentID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? SchoolID { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(100)]
        public string TransactionNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        public long? SHeadID { get; set; }
        public long? DHeadID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? FeeCollectionID { get; set; }
        public long? FeeCollectionFeeTypeMapsID { get; set; }
        public int? FeeMasterID { get; set; }
        public long? SlNo { get; set; }
    }
}
