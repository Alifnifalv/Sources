using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("NotebooksBundleSalesExcelData")]
    public partial class NotebooksBundleSalesExcelData
    {
        public double? FeeMasterID { get; set; }
        [StringLength(255)]
        public string FeeName { get; set; }
        public double? SchoolID { get; set; }
        [StringLength(255)]
        public string SchoolName { get; set; }
        public double? ClassID { get; set; }
        [StringLength(255)]
        public string ClassName { get; set; }
        public double? SectionID { get; set; }
        [StringLength(255)]
        public string SectionName { get; set; }
        public double? StudentID { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        public double? StudentFeeDueID { get; set; }
        public double? FeeDueFeeTypeMapsIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(255)]
        public string InvoiceNo { get; set; }
        public double? CollectedAmount { get; set; }
        public double? ProductSKUMapID { get; set; }
        [StringLength(255)]
        public string F18 { get; set; }
        [StringLength(255)]
        public string F19 { get; set; }
    }
}
