using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ChartOfAccountMaps", Schema = "account")]
    public partial class ChartOfAccountMap
    {
        [Key]
        public long ChartOfAccountMapIID { get; set; }
        public long? ChartOfAccountID { get; set; }
        public long? AccountID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(20)]
        public string AccountCode { get; set; }
        public int? IncomeOrBalance { get; set; }
        public int? ChartRowTypeID { get; set; }
        public int? NoOfBlankLines { get; set; }
        public bool? IsNewPage { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("ChartOfAccountMaps")]
        public virtual Account Account { get; set; }
        [ForeignKey("ChartOfAccountID")]
        [InverseProperty("ChartOfAccountMaps")]
        public virtual ChartOfAccount ChartOfAccount { get; set; }
        [ForeignKey("ChartRowTypeID")]
        [InverseProperty("ChartOfAccountMaps")]
        public virtual ChartRowType ChartRowType { get; set; }
    }
}
