using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LocationTypes", Schema = "inventory")]
    public partial class LocationType
    {
        public LocationType()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        public byte LocationTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("LocationType")]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
