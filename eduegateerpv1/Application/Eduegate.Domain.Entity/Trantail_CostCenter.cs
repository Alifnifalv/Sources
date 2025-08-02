namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Trantail_CostCenter")]
    public partial class Trantail_CostCenter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TL_CostID { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ref_TH_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ref_SlNo { get; set; }

        public int? AccountID { get; set; }

        public int? CostCenterID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SlNo { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public int? Ref_ID { get; set; }
    }
}
