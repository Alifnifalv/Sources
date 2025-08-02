using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentMethodSiteMaps", Schema = "mutual")]
    public partial class PaymentMethodSiteMap
    {
        [Key]
        public int SiteID { get; set; }
        [Key]
        public short PaymentMethodID { get; set; }
        public int? SortOrder { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("PaymentMethodID")]
        [InverseProperty("PaymentMethodSiteMaps")]
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
