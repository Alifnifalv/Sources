namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.Notify")]
    public partial class Notify
    {
        [Key]
        public long NotifyIID { get; set; }

        public long? ProductSKUMapID { get; set; }

        [StringLength(100)]
        public string EmailID { get; set; }

        public int? SiteID { get; set; }

        public bool? IsEmailSend { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CompanyID { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual Site Site { get; set; }
    }
}
