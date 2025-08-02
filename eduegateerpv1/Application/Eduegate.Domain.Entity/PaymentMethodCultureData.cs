namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.PaymentMethodCultureDatas")]
    public partial class PaymentMethodCultureData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PaymentMethodID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        public string ImageName { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual PaymentMethod PaymentMethod1 { get; set; }
    }
}
