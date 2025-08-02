using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BookCancellation_20230806
    {
        public double? FeeMasterID { get; set; }
        [StringLength(255)]
        public string FeeName { get; set; }
        public double? StudentIID { get; set; }
        public double? AcademicYearID { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [StringLength(255)]
        public string InvoiceNo { get; set; }
        public double? StudentFeeDueIID { get; set; }
        public double? FeeDueFeeTypeMapsIID { get; set; }
        public double? Due_Amount { get; set; }
        public double? Crn_Amount { get; set; }
        public double? Col_Amount { get; set; }
        public double? AcademicYearCode { get; set; }
        public double? Fee_Balance { get; set; }
        [StringLength(255)]
        public string REMARKS { get; set; }
    }
}
