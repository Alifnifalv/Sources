using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceProviderLog
    {
        public long ServiceProviderLogIID { get; set; }
        public Nullable<int> ServiceProviderID { get; set; }
        public Nullable<long> ReferenceDocumentID { get; set; }
        public Nullable<System.DateTime> LogDateTime { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}
