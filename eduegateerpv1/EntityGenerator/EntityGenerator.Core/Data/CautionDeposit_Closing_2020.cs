using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CautionDeposit_Closing_2020
    {
        public double? SlNo { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        [StringLength(255)]
        public string AcademicYear { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [StringLength(255)]
        public string Status { get; set; }
    }
}
