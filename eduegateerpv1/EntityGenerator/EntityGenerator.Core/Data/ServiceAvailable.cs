using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ServiceAvailables", Schema = "saloon")]
    public partial class ServiceAvailable
    {
        public ServiceAvailable()
        {
            Services = new HashSet<Service>();
        }

        [Key]
        public int ServiceAvailableID { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("ServiceAvailable")]
        public virtual ICollection<Service> Services { get; set; }
    }
}
