using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Zones", Schema = "mutual")]
    public partial class Zone
    {
        public Zone()
        {
            DeliveryTypeAllowedZoneMaps = new HashSet<DeliveryTypeAllowedZoneMap>();
            PaymentExceptionByZoneDeliveries = new HashSet<PaymentExceptionByZoneDelivery>();
        }

        [Key]
        public short ZoneID { get; set; }
        [StringLength(50)]
        public string ZoneName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public int? CountryID { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("Zones")]
        public virtual Country Country { get; set; }
        [InverseProperty("Zone")]
        public virtual ICollection<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }
        [InverseProperty("Zone")]
        public virtual ICollection<PaymentExceptionByZoneDelivery> PaymentExceptionByZoneDeliveries { get; set; }
    }
}
