using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceAvailable
    {
        public ServiceAvailable()
        {
            this.Services = new List<Service>();
        }

        public int ServiceAvailableID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
