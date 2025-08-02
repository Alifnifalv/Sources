using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class LocationType
    {
        public LocationType()
        {
            this.Locations = new List<Location>();
        }

        public byte LocationTypeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
