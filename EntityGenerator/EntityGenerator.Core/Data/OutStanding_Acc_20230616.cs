using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class OutStanding_Acc_20230616
    {
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? Period_ID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Period_Caption { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(500)]
        public string CompanyCode { get; set; }
        [StringLength(500)]
        public string CompanyName { get; set; }
        public int? FiscalYear_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FiscalYear_Name { get; set; }
        public long GL_AccountID { get; set; }
        [Required]
        [StringLength(30)]
        [Unicode(false)]
        public string GL_AccountCode { get; set; }
        [StringLength(500)]
        public string GL_AccountName { get; set; }
        public long SL_AccountID { get; set; }
        [StringLength(50)]
        public string SL_AccountCode { get; set; }
        [StringLength(100)]
        public string SL_AccountName { get; set; }
        [Column(TypeName = "money")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
