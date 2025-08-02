namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.PaymentModes_20211221")]
    public partial class PaymentModes_20211221
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentModeID { get; set; }

        [StringLength(50)]
        public string PaymentModeName { get; set; }

        public long? AccountId { get; set; }

        public int? TenderTypeID { get; set; }
    }
}
