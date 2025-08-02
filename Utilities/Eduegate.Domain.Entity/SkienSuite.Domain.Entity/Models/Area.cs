using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Area
    {
        public Area()
        {
            this.DeliveryTypeAllowedAreaMaps = new List<DeliveryTypeAllowedAreaMap>();
            this.OrderContactMaps = new List<OrderContactMap>();
        }

        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public Nullable<short> ZoneID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> RouteID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
    }
}
