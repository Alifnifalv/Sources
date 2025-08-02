using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductDeliveryCountrySettings", Schema = "inventory")]
    public partial class ProductDeliveryCountrySetting
    {
        [Key]
        public long ProductDeliveryCountrySettingsIID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public long? CountryID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductDeliveryCountrySettings")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductDeliveryCountrySettings")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
