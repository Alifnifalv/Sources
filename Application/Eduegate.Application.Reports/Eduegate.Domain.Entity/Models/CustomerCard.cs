namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.CustomerCards")]
    public partial class CustomerCard
    {
        [Key]
        public long CustomerCardIID { get; set; }

        public int? CardTypeID { get; set; }

        public long? LoginID { get; set; }

        public long? CustomerID { get; set; }

        [StringLength(50)]
        public string CardNumber { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string ExternalCode1 { get; set; }

        [StringLength(50)]
        public string ExternalCode2 { get; set; }

        public virtual Login Login { get; set; }

        public virtual CardType CardType { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
