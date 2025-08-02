using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListCustomerMaps", Schema = "catalog")]
    public partial class ProductPriceListCustomerMap
    {
        [Key]
        public long ProductPriceListCustomerMapIID { get; set; }
        public long? ProductPriceListID { get; set; }
        public long? CustomerID { get; set; }
        public byte? EntitlementID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("ProductPriceListCustomerMaps")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("EntitlementID")]
        [InverseProperty("ProductPriceListCustomerMaps")]
        public virtual EntityTypeEntitlement Entitlement { get; set; }
        [ForeignKey("ProductPriceListID")]
        [InverseProperty("ProductPriceListCustomerMaps")]
        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
