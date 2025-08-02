using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class STUDENT_PRODUCT_CLASS_FEE_RELATION
    {
        public int? SchoolID { get; set; }
        [StringLength(100)]
        public string SchoolName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(100)]
        public string AcademicYear { get; set; }
        public long? StudentID { get; set; }
        [StringLength(100)]
        public string AdmissionNumber { get; set; }
        [StringLength(100)]
        public string StudentName { get; set; }
        public int? ClassID { get; set; }
        [StringLength(100)]
        public string ClassName { get; set; }
        public int? FeeMasterID { get; set; }
        [StringLength(100)]
        public string FeeMasterName { get; set; }
        [Column(TypeName = "money")]
        public decimal? FeeAmount { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [StringLength(100)]
        public string SKUName { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        public int? SecondLangID { get; set; }
        [StringLength(100)]
        public string SecondLanguage { get; set; }
        public int? ThirdLangID { get; set; }
        [StringLength(100)]
        public string ThirdLanguage { get; set; }
        public int? SubjectMapID { get; set; }
        [StringLength(100)]
        public string SubjectMap { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        public long? SHeadID { get; set; }
        public long? DHeadID { get; set; }
        public long? RowIndex { get; set; }
    }
}
