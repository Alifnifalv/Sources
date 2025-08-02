using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("LocationTypes", Schema = "inventory")]
    public partial class LocationType
    {
        public LocationType()
        {
            this.Locations = new List<Location>();
        }

        [Key]
        public byte LocationTypeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
