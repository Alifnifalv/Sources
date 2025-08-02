namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.ChartOfAccountMaps")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Account Account { get; set; }

        public virtual ChartOfAccount ChartOfAccount { get; set; }

        public virtual ChartRowType ChartRowType { get; set; }
    }
}
