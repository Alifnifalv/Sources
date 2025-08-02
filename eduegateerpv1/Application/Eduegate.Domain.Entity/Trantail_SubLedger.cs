namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Trantail_SubLedger")]
    public partial class Trantail_SubLedger
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TL_SL_ID { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Ref_TH_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ref_SlNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SlNo { get; set; }

        public int AccountID { get; set; }

        public int SL_AccountID { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public long Ref_ID { get; set; }

        public int Correspond_AccountID { get; set; }
    }
}
