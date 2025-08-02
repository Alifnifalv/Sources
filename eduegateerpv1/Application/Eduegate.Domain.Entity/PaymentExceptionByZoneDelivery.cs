namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment.PaymentExceptionByZoneDelivery")]
    public partial class PaymentExceptionByZoneDelivery
    {
        [Key]
        public long PaymentExceptionByZoneDeliveryIID { get; set; }

        public short ZoneID { get; set; }

        public int DeliveryTypeID { get; set; }

        public short PaymentMethodID { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamps { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual Zone Zone { get; set; }
    }
}
