namespace Eduegate.Domain.Entity
{
    using Eduegate.Domain.Entity.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DeliveryTypeCultureDatas", Schema = "inventory")]
    public partial class DeliveryTypeCultureData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeliveryTypeID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        [StringLength(200)]
        public string DeliveryTypeName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}
