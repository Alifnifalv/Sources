using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DeliveryCharges", Schema = "inventory")]
    public partial class DeliveryCharge
    {
        [Key]
        public long DeliveryChargeIID { get; set; }
        public Nullable<int> ServiceProviderID { get; set; }
        public Nullable<int> FromCountryID { get; set; }
        public Nullable<int> ToCountryID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<decimal> CartStartRange { get; set; }
        public Nullable<decimal> CartEndRange { get; set; }
        public Nullable<decimal> WeightStartRange { get; set; }
        public Nullable<decimal> WeightEndRange { get; set; }
        public Nullable<decimal> DeliveryCharge1 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual Country FromCountry { get; set; }
        public virtual Country ToCountry { get; set; }
        public virtual DeliveryTypes1 DeliveryType { get; set; }
    }
}
