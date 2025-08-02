using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DEFAULTER_CREDITNOTE_202201_1
    {
        [Column(TypeName = "datetime")]
        public DateTime? CreditNoteDate { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long CreditNoteFeeTypeMapID { get; set; }
        public long StudentIID { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int PeriodID { get; set; }
        public int FeePeriodID { get; set; }
    }
}
