using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Zone
    {
        public Zone()
        {
            this.DeliveryTypeAllowedZoneMaps = new List<DeliveryTypeAllowedZoneMap>();
            this.Areas = new List<Area>();
        }

        public short ZoneID { get; set; }
        public string ZoneName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public virtual ICollection<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual Country Country { get; set; }
    }
}
