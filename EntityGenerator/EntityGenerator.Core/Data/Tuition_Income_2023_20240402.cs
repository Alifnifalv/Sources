using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Tuition_Income_2023_20240402
    {
        public int? DocumentTypeID { get; set; }
        public int? CompanyID { get; set; }
        public int? FiscalYear_ID { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public long TH_ID { get; set; }
        public int AccountID { get; set; }
        public int SL_AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? SL_Amount { get; set; }
        public long StudentID { get; set; }
        public int ClassID { get; set; }
        public int SectionID { get; set; }
        public int AcademicYearID { get; set; }
        public int SchoolID { get; set; }
    }
}
