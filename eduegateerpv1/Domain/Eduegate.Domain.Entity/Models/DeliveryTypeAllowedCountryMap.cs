using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DeliveryTypeAllowedCountryMaps", Schema = "inventory")]
    public partial class DeliveryTypeAllowedCountryMap
    {
        [Key]
        public int DeliveryTypeID { get; set; }
        public int FromCountryID { get; set; }
        public int ToCountryID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Country Country { get; set; }
        public virtual Country Country1 { get; set; }
        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}
