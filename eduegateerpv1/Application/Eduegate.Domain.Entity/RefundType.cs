namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.RefundTypes")]
    public partial class RefundType
    {
        public int RefundTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string RefundTypeName { get; set; }
    }
}
