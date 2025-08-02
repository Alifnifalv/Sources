using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentMethodSiteMaps", Schema = "mutual")]
    public partial class PaymentMethodSiteMap
    {
        [Key]
        public int SiteID { get; set; }
        public short PaymentMethodID { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Site Site { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
