using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Notify", Schema = "inventory")]
    public partial class Notify
    {
        [Key]
        public long NotifyIID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [StringLength(100)]
        public string EmailID { get; set; }
        public int? SiteID { get; set; }
        public bool? IsEmailSend { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("Notifies")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        [ForeignKey("SiteID")]
        [InverseProperty("Notifies")]
        public virtual Site Site { get; set; }
    }
}
