using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ServiceProviderLogs", Schema = "distribution")]
    public partial class ServiceProviderLog
    {
        [Key]
        public long ServiceProviderLogIID { get; set; }
        public Nullable<int> ServiceProviderID { get; set; }
        public Nullable<System.DateTime> LogDateTime { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
    }
}
