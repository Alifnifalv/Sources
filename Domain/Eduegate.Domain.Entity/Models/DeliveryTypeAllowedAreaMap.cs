using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DeliveryTypeAllowedAreaMaps", Schema = "inventory")]
    public partial class DeliveryTypeAllowedAreaMap
    {
        [Key]
        public long AreaDeliveryTypeMapIID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<int> AreaID { get; set; }
        public Nullable<decimal> CartTotalFrom { get; set; }
        public Nullable<decimal> CartTotalTo { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<decimal> DeliveryChargePercentage { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public virtual Area Area { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}
