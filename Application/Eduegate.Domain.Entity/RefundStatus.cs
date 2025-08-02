namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.RefundStatuses")]
    public partial class RefundStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RefundStatusID { get; set; }

        [Required]
        [StringLength(50)]
        public string RefundStatusName { get; set; }
    }
}
