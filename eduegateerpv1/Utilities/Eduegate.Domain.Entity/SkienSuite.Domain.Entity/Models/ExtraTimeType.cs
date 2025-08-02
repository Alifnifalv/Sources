using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ExtraTimeType
    {
        public ExtraTimeType()
        {
            this.Services = new List<Service>();
        }

        public int ExtraTimeTypeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
