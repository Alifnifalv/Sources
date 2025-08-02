

namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeePaymentModes", Schema = "schools")]
    public partial class FeePaymentModes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte PaymentModeID { get; set; }

        [StringLength(50)]
        public string PaymentModeName { get; set; }

    }
}
