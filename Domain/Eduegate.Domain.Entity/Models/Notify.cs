using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Notify", Schema = "inventory")]
    public partial class Notify
    {
        [Key]
        public long NotifyIID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public string EmailID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<bool> IsEmailSend { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Site Site { get; set; }
    }
}
