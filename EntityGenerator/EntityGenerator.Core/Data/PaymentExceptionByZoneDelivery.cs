using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentExceptionByZoneDelivery", Schema = "payment")]
    public partial class PaymentExceptionByZoneDelivery
    {
        [Key]
        public long PaymentExceptionByZoneDeliveryIID { get; set; }
        public short ZoneID { get; set; }
        public int DeliveryTypeID { get; set; }
        public short PaymentMethodID { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }

        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("PaymentExceptionByZoneDeliveries")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("PaymentMethodID")]
        [InverseProperty("PaymentExceptionByZoneDeliveries")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        [ForeignKey("ZoneID")]
        [InverseProperty("PaymentExceptionByZoneDeliveries")]
        public virtual Zone Zone { get; set; }
    }
}
