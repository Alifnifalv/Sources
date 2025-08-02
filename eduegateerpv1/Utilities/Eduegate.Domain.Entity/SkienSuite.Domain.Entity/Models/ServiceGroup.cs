using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceGroup
    {
        public ServiceGroup()
        {
            this.Services = new List<Service>();
        }

        public long ServiceGroupIID { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
