using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeCultureDatas", Schema = "inventory")]
    public partial class DeliveryTypeCultureData
    {
        [Key]
        public int DeliveryTypeID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(200)]
        public string DeliveryTypeName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("DeliveryTypeCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("DeliveryTypeCultureDatas")]
        public virtual DeliveryType1 DeliveryType { get; set; }
    }
}
