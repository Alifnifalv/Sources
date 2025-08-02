namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketBankDetails")]
    public partial class TicketBankDetail
    {
        [Key]
        public long BankIID { get; set; }

        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [StringLength(100)]
        public string AccountNumber { get; set; }

        public long ReferenceDetailID { get; set; }

        public decimal RefundAmount { get; set; }
    }
}
