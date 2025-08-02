using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerGroups", Schema = "mutual")]
    public partial class CustomerGroup
    {
        public CustomerGroup()
        {
            CustomerGroupDeliveryTypeMaps = new HashSet<CustomerGroupDeliveryTypeMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
            ProductPriceListSKUMaps = new HashSet<ProductPriceListSKUMap>();
        }

        [Key]
        public long CustomerGroupIID { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PointLimit { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("CustomerGroup")]
        public virtual ICollection<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
        [InverseProperty("CustomerGroup")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        [InverseProperty("CustomerGroup")]
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
    }
}
