using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_Fee_Outstanding_Current
    {
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string InvoiceDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TranDate { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long ParentID { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        public long? ParentLoginID { get; set; }
        [StringLength(500)]
        public string ParentEmail { get; set; }
        [StringLength(302)]
        public string FatherName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PhoneNumber { get; set; }
        public int FeeMasterID { get; set; }
        [StringLength(50)]
        public string FeeName { get; set; }
        public byte FeeCycleID { get; set; }
        [StringLength(50)]
        public string Fee_Due { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal Fee_Col { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal Fee_Crn { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal Fee_Stl { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Fee_Bal { get; set; }
    }
}
